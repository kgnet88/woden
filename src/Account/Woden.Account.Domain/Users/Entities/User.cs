namespace KgNet88.Woden.Account.Domain.Users.Entities;

/// <summary>
/// Central entity and aggregate root for the account domain.
/// </summary>
public record User
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// The users credentials for login.
    /// </summary>
    public required Credentials Credentials { get; init; }

    /// <summary>
    /// Last login timepoint into the account.
    /// </summary>
    public required ZonedDateTime LastLogin { get; init; }
}
