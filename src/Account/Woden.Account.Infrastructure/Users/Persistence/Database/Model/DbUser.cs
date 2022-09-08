namespace KgNet88.Woden.Account.Infrastructure.Users.Persistence.Database.Model;

/// <summary>
/// Database model for a identity user with extensions for the account BC.
/// </summary>
public class DbUser : IdentityUser<Guid>
{
    /// <summary>
    /// Last successful login.
    /// </summary>
    public Instant LastLogin { get; set; }

    /// <summary>
    /// Many to one relationship to user claim table.
    /// </summary>
    public virtual ICollection<DbUserClaim>? Claims { get; set; }

    /// <summary>
    /// Many to one relationship to user login table.
    /// </summary>
    public virtual ICollection<DbUserLogin>? Logins { get; set; }

    /// <summary>
    /// Many to one relationship to user token table.
    /// </summary>
    public virtual ICollection<DbUserToken>? Tokens { get; set; }

    /// <summary>
    /// Many to one relationship to user role table.
    /// </summary>
    public virtual ICollection<DbUserRole>? UserRoles { get; set; }
}
