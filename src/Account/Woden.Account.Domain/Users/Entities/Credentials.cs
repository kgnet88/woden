namespace KgNet88.Woden.Account.Domain.Users.Entities;

/// <summary>
/// The user credentials and additional informations for using the account.
/// </summary>
public record Credentials
{
    /// <summary>
    /// Username to log into the account.
    /// </summary>
    public required string Username { get; init; }

    /// <summary>
    /// Email to confirm account issues.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Last login timepoint into the account.
    /// </summary>
    public required ZonedDateTime LastLogin { get; init; }
}
