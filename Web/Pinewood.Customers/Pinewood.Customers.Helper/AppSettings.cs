namespace Pinewood.Customers.Helper;

public class AppSettings
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? SigningKey { get; set; }

    public int ExpirationDays { get; set; }
}