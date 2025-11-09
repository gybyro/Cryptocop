namespace Cryptocop.Software.API.Services.Helpers;

public class RabbitMqSettings
{
    public string HostName { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string ExchangeName { get; init; } = string.Empty;
    public string RoutingKey { get; init; } = string.Empty;
}