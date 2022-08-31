namespace Goedde88.Woden.User.Api.Auth.Infrastructure.Database.Model;

public class DbUserRole : IdentityUserRole<Guid>
{
    public virtual DbUser? User { get; init; }
    public virtual DbRole? Role { get; init; }
}
