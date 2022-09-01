namespace KgNet88.Woden.Account.Api.Auth.Infrastructure.Database.Model;

public class DbRole : IdentityRole<Guid>
{
    public virtual ICollection<DbUserRole>? UserRoles { get; init; }
    public virtual ICollection<DbRoleClaim>? RoleClaims { get; init; }
}