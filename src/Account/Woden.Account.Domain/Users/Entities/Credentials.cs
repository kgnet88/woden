namespace KgNet88.Woden.Account.Domain.Users.Entities;

/// <summary>
/// The user credentials and additional informations for using the account.
/// </summary>
public class Credentials : ValueObject
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
    /// Returns the list of properties, which are used to identify equality.
    /// </summary>
    /// <returns>list of properties, which decide wether two value objects are equal.</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Username;
        yield return this.Email;
    }
}
