namespace KgNet88.Woden.Account.Infrastructure.Users.Persistence.Database.Model;

/// <summary>
/// Database model for a identity user with extensions for the account BC.
/// </summary>
public class DbUserClaim : IdentityUserClaim<Guid>
{
    /// <summary>
    /// Foreign key to user table.
    /// </summary>
    public virtual DbUser? User { get; set; }
}
