using System.Linq;
using System.Text;
using System.Text.Json;
using CreditCardValidator;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.Worker.Payments.Options;

namespace Cryptocop.Software.Worker.Payments;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly RabbitMqOptions _rabbitOptions;
    private readonly JsonSerializerOptions _serializerOptions = new(JsonSerializerDefaults.Web);
    private IConnection? _connection;
    private IModel? _channel;
    public Worker(ILogger<Worker> logger, IOptions<RabbitMqOptions> rabbitOptions)
    {
        _logger = logger;
        _rabbitOptions = rabbitOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        InitialiseRabbitMq();

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += HandleMessageAsync;

        _channel!.BasicConsume(queue: _rabbitOptions.QueueName, autoAck: false, consumer: consumer);
        _logger.LogInformation("Payment Worker listening for messages on {queue}", _rabbitOptions.QueueName);

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (TaskCanceledException) { }
    }

    private Task HandleMessageAsync(object sender, BasicDeliverEventArgs args)
    {
        try
        {
            var message = Encoding.UTF8.GetString(args.Body.Span);
            var order = JsonSerializer.Deserialize<OrderDto>(message, _serializerOptions);

            if (order == null)
            {
                _logger.LogError("Unable to deserialize order message: {Message}", message);
                _channel?.BasicNack(args.DeliveryTag, multiple: false, requeue: false);
                return Task.CompletedTask;
            }

            var sanitizedNumber = new string(order.CreditCard.Where(char.IsDigit).ToArray());
            if (string.IsNullOrWhiteSpace(sanitizedNumber))
            {
                _logger.LogWarning("Received order without a usable credit card number for {Email}.", order.Email);
                _channel?.BasicAck(args.DeliveryTag, multiple: false);
                return Task.CompletedTask;
            }
            var detector = new CreditCardDetector(sanitizedNumber);
            var displayNumber = string.IsNullOrWhiteSpace(sanitizedNumber) ? order.CreditCard : sanitizedNumber;
            var lastDigits = displayNumber.Length <= 4 ? displayNumber : displayNumber[^4..];
            var validationMessage = detector.IsValid()
                ? $"Credit card ending with {lastDigits} is valid ({detector.BrandName})."
                : $"Credit card ending with {lastDigits} is invalid.";


            if (detector.IsValid()) _logger.LogInformation(validationMessage);
            else _logger.LogWarning(validationMessage);

            _channel?.BasicAck(args.DeliveryTag, multiple: false);

        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing a payment message.");
            _channel?.BasicNack(args.DeliveryTag, multiple: false, requeue: false);
        }

        return Task.CompletedTask;
    }

    private void InitialiseRabbitMq()
    {
        var factory = new ConnectionFactory
        {
            HostName = _rabbitOptions.HostName,
            UserName = _rabbitOptions.UserName,
            Password = _rabbitOptions.Password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchange: _rabbitOptions.ExchangeName, type: ExchangeType.Topic, durable: true, autoDelete: false);
        _channel.QueueDeclare(queue: _rabbitOptions.QueueName, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind(queue: _rabbitOptions.QueueName, exchange: _rabbitOptions.ExchangeName, routingKey: _rabbitOptions.RoutingKey);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _channel?.Close();
        _connection?.Close();
        return base.StopAsync(cancellationToken);
    }

    public override void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
        base.Dispose();
    }
}

