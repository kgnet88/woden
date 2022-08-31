namespace Goedde88.Woden.User.Api.Auth.Infrastructure.Database.Model;

public class DbUser : IdentityUser<Guid>
{
    public virtual ICollection<DbUserClaim>? Claims { get; init; }
    public virtual ICollection<DbUserLogin>? Logins { get; init; }
    public virtual ICollection<DbUserToken>? Tokens { get; init; }
    public virtual ICollection<DbUserRole>? UserRoles { get; init; }
}
