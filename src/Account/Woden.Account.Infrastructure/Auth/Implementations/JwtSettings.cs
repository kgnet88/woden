namespace KgNet88.Woden.Account.Infrastructure.Auth.Implementations;

public record JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public string Secret { get; init; } = default!;

    public int ExpiryMinutes { get; init; } = 20;
}
