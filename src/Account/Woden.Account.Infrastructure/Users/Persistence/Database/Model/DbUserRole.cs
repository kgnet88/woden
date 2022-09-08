namespace KgNet88.Woden.Account.Infrastructure.Users.Persistence.Database.Model;

/// <summary>
/// Database model for a identity user with extensions for the account BC.
/// </summary>
public class DbUserRole : IdentityUserRole<Guid>
{
    /// <summary>
    /// Foreign key to user table.
    /// </summary>
    public virtual DbUser? User { get; set; }

    /// <summary>
    /// Foreign key to role table.
    /// </summary>
    public virtual DbRole? Role { get; set; }
}
