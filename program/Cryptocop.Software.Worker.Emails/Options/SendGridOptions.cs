namespace Cryptocop.Software.Worker.Emails.Options;

public class SendGridOptions
{
    public string ApiKey { get; init; } = string.Empty;
    public string FromEmail { get; init; } = string.Empty;
    public string FromName { get; init; } = string.Empty;
}