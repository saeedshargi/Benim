namespace Benim.Models;

public class JwtConfiguration
{
    public const string JwtSection = "JwtConfiguration";

    public bool ValidateIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public bool ValidateLifetime { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }
    public string ValidIssuer { get; set; } = string.Empty;
    public string ValidAudience { get; set; } = string.Empty;
    public string IssuerSigningKey { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
    public int RefreshTokenDurationInDay { get; set; }
}