using System.Text;
using System.Text.Json;
using System.Threading;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

using Cryptocop.Software.API.Services.Helpers;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Services.Implementations;

public class QueueService : IQueueService, IDisposable
{
    private readonly RabbitMqSettings _settings;
    private readonly Lazy<IConnection> _connection;
    private readonly Lazy<IModel> _channel;
    private readonly JsonSerializerOptions _serializerOptions = new(JsonSerializerDefaults.Web);

    public QueueService(IOptions<RabbitMqSettings> options)
    {
        _settings = options.Value;

        _connection = new Lazy<IConnection>(CreateConnection, LazyThreadSafetyMode.ExecutionAndPublication);
        _channel = new Lazy<IModel>(CreateChannel, LazyThreadSafetyMode.ExecutionAndPublication);
    }

    public Task PublishMessageAsync(string routingKey, object body)
    {
        if (string.IsNullOrWhiteSpace(routingKey))
        {
            throw new ArgumentException("A routing key is required when publishing a message.", nameof(routingKey));
        }

        EnsureChannel();

        var payload = JsonSerializer.Serialize(body, _serializerOptions);
        var messageBody = Encoding.UTF8.GetBytes(payload);

        var properties = _channel.Value.CreateBasicProperties();
        properties.Persistent = true;
        properties.ContentType = "application/json";

        _channel.Value.BasicPublish(
            exchange: _settings.ExchangeName,
            routingKey: routingKey,
            basicProperties: properties,
            body: messageBody);

        return Task.CompletedTask;
    }

    private IConnection CreateConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password,
            DispatchConsumersAsync = true
        };

        return factory.CreateConnection();
    }

    private IModel CreateChannel()
    {
        var channel = _connection.Value.CreateModel();
        channel.ExchangeDeclare(
            exchange: _settings.ExchangeName,
            type: ExchangeType.Topic,
            durable: true,
            autoDelete: false);
        return channel;
    }

    private void EnsureChannel()
    {
        _ = _channel.Value;
    }

    public void Dispose()
    {
        if (_channel.IsValueCreated)
        {
            _channel.Value.Dispose();
        }

        if (_connection.IsValueCreated)
        {
            _connection.Value.Dispose();
        }
    }
}