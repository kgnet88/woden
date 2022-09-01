namespace KgNet88.Woden.Account.Contracts.Auth.Responses;

public record LoginResponse
{
    /// <summary>
    /// access token for authorization
    /// </summary>
    public required string AccessToken { get; init; }

    /// <summary>
    /// timestamp how long the token is valid
    /// </summary>
    public required Instant ValidTo { get; init; }
}
