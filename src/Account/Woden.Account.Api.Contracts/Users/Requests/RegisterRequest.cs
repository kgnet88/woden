namespace KgNet88.Woden.Account.Api.Contracts.Users.Requests;

/// <summary>
/// To register an user for an account.
/// </summary>
public record RegisterRequest
{
    /// <summary>
    /// Unique username for login.
    /// </summary>
    public string Username { get; init; } = default!;

    /// <summary>
    /// Unique email for confirmation.
    /// </summary>
    public string Email { get; init; } = default!;

    /// <summary>
    /// Password with at least 8 letters and all four categories.
    /// </summary>
    public string Password { get; init; } = default!;
}