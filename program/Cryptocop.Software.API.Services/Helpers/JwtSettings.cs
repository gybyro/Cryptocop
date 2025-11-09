namespace Cryptocop.Software.API.Services.Helpers;

public class JwtSettings
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string SecretKey { get; set; }
    public int TokenLifetimeMinutes { get; set; }
}