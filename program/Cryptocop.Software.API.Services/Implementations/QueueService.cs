using System.Text.Json;

using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Services.Implementations;

public class QueueService : IQueueService, IDisposable
{
    public async Task PublishMessageAsync(string routingKey, object body)
    {
        string jsn = JsonSerializer.Serialize(body);

        // TODO:
        // Publish the message using a channel created with the RabbitMQ client
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}