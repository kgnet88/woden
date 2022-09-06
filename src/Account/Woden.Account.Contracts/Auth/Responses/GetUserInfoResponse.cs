namespace KgNet88.Woden.Account.Contracts.Auth.Responses;

public record GetUserInfoResponse
{
    /// <summary>
    /// username of the given user
    /// </summary>
    public required string Username { get; init; }

    /// <summary>
    /// email of the given user
    /// </summary>
    public required string Email { get; init; }
}
