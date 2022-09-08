namespace KgNet88.Woden.Account.Infrastructure.Users.Implementations;

/// <summary>
/// Configuration object for the necessary propereties to generate jason web tokens.
/// </summary>
public record JwtSettings
{
    /// <summary>
    /// Section name inside the configuration.
    /// </summary>
    public const string SectionName = "JwtSettings";

    /// <summary>
    /// The JWT issuer (who gives this token out).
    /// </summary>
    public string Issuer { get; init; } = default!;

    /// <summary>
    /// The JWT audience (for whom is this token for).
    /// </summary>
    public string Audience { get; init; } = default!;

    /// <summary>
    /// The secret key to secure the token.
    /// </summary>
    public string Secret { get; init; } = default!;

    /// <summary>
    /// The expiry time of the token.
    /// </summary>
    public int ExpiryMinutes { get; init; } = 20;
}
