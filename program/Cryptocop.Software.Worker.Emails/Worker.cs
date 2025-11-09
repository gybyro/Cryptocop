
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SendGrid;
using SendGrid.Helpers.Mail;

using Cryptocop.Software.Worker.Emails.Options;
using Cryptocop.Software.API.Models.Dtos;


namespace Cryptocop.Software.Worker.Emails;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly RabbitMqOptions _rabbitOptions;
    private readonly SendGridOptions _sendGridOptions;
    private readonly ISendGridClient _sendGridClient;
    private readonly JsonSerializerOptions _serializerOptions = new(JsonSerializerDefaults.Web);
    private IConnection? _connection;
    private IModel? _channel;

    public Worker(
        ILogger<Worker> logger,
        IOptions<RabbitMqOptions> rabbitOptions,
        IOptions<SendGridOptions> sendGridOptions,
        ISendGridClient sendGridClient)
    {
        _logger = logger;
        _rabbitOptions = rabbitOptions.Value;
        _sendGridOptions = sendGridOptions.Value;
        _sendGridClient = sendGridClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        InitialiseRabbitMq();

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += HandleMessageAsync;

        _channel!.BasicConsume(queue: _rabbitOptions.QueueName, autoAck: false, consumer: consumer);
        _logger.LogInformation("📧 Email Worker listening for messages on {queue}", _rabbitOptions.QueueName);

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (TaskCanceledException) { }
    }

    // HANDLE IT
    private async Task HandleMessageAsync(object sender, BasicDeliverEventArgs args)
    {
        try
        {
            var message = Encoding.UTF8.GetString(args.Body.Span);
            var order = JsonSerializer.Deserialize<OrderDto>(message, _serializerOptions);

            if (order == null)
            {
                _logger.LogError("Could not deserialize order message: {Message}", message);
                _channel?.BasicNack(args.DeliveryTag, multiple: false, requeue: false);
                return;
            }

            var email = BuildEmail(order);
            var response = await _sendGridClient.SendEmailAsync(email);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("✅ Sent order confirmation email to {Email}", order.Email);
                _channel?.BasicAck(args.DeliveryTag, multiple: false);
            }
            else
            {
                _logger.LogError("❌ Failed to send email. Status: {Status}", response.StatusCode);
                _channel?.BasicNack(args.DeliveryTag, multiple: false, requeue: false);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while sending email.");
            _channel?.BasicNack(args.DeliveryTag, multiple: false, requeue: false);
        }
    }
    

    // Sends the SendGrid a message with the stuffs in it...
    private SendGridMessage BuildEmail(OrderDto order)
    {
        var subject = $"Your Cryptocop order #{order.Id} confirmation";
        var from = new EmailAddress(_sendGridOptions.FromEmail, _sendGridOptions.FromName);
        var to = new EmailAddress(order.Email, order.FullName);

        var orderItemsRows = string.Join(string.Empty, order.OrderItems.Select(item =>
            $"<tr><td>{WebUtility.HtmlEncode(item.ProductIdentifier)}</td><td>{item.Quantity}</td><td>{item.UnitPrice:C2}</td><td>{item.TotalPrice:C2}</td></tr>"));

        var htmlContent = $@"<!DOCTYPE html>
            <html lang=\'en\'>
            <head>
                <meta charset=\'UTF-8\'>
                <title>Order confirmation</title>
                <style>
                body {{ font-family: Arial, sans-serif; color: #222; }}
                .container {{ max-width: 600px; margin: 0 auto; }}
                h1 {{ color: #0b7285; }}
                table {{ width: 100%; border-collapse: collapse; margin-top: 16px; }}
                th, td {{ border: 1px solid #ddd; padding: 8px; text-align: left; }}
                th {{ background-color: #f1f3f5; }}
                </style>
            </head>
            <body>
                <div class=\'container\'>
                <h1>Thank you for your order, {WebUtility.HtmlEncode(order.FullName)}!</h1>
                <p>Your order placed on {WebUtility.HtmlEncode(order.OrderDate)} has been successfully processed.</p>
                <h2>Shipping information</h2>
                <p>
                    {WebUtility.HtmlEncode(order.StreetName)} {WebUtility.HtmlEncode(order.HouseNumber)}<br/>
                    {WebUtility.HtmlEncode(order.ZipCode)} {WebUtility.HtmlEncode(order.City)}<br/>
                    {WebUtility.HtmlEncode(order.Country)}
                </p>
                <h2>Order summary</h2>
                <p><strong>Total price:</strong> {order.TotalPrice:C2}</p>
                <table>
                    <thead>
                    <tr>
                        <th>Item</th>
                        <th>Quantity</th>
                        <th>Unit price</th>
                        <th>Total</th>
                    </tr>
                    </thead>
                    <tbody>
                    {orderItemsRows}
                    </tbody>
                </table>
                </div>
            </body>
            </html>";

        var plainTextContent = new StringBuilder()
            .AppendLine($"Thank you for your order, {order.FullName}!")
            .AppendLine($"Order date: {order.OrderDate}")
            .AppendLine($"Total price: {order.TotalPrice:C2}")
            .AppendLine("Shipping information:")
            .AppendLine($"{order.StreetName} {order.HouseNumber}")
            .AppendLine($"{order.ZipCode} {order.City}")
            .AppendLine(order.Country)
            .AppendLine()
            .AppendLine("Order items:")
            .AppendLine(string.Join(Environment.NewLine, order.OrderItems.Select(item => $"- {item.ProductIdentifier} x{item.Quantity} @ {item.TotalPrice:C2}")))
            .ToString();

        return MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
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

        _channel.ExchangeDeclare(_rabbitOptions.ExchangeName, ExchangeType.Topic, durable: true);
        _channel.QueueDeclare(_rabbitOptions.QueueName, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind(_rabbitOptions.QueueName, _rabbitOptions.ExchangeName, _rabbitOptions.RoutingKey);
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