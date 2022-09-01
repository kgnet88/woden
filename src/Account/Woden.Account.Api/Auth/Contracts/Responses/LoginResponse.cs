namespace Goedde88.Woden.Account.Api.Auth.Contracts.Responses;

public class LoginResponse
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
