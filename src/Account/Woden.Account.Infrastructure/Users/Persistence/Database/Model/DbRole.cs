namespace KgNet88.Woden.Account.Infrastructure.Users.Persistence.Database.Model;

/// <summary>
/// Database model for a identity user with extensions for the account BC.
/// </summary>
public class DbRole : IdentityRole<Guid>
{
    /// <summary>
    /// Many to one relationship to user role table.
    /// </summary>
    public virtual ICollection<DbUserRole>? UserRoles { get; set; }

    /// <summary>
    /// Many to one relationship to role claim table.
    /// </summary>
    public virtual ICollection<DbRoleClaim>? RoleClaims { get; set; }
}