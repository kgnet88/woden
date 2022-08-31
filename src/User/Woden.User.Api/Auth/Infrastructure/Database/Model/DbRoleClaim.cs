namespace Goedde88.Woden.User.Api.Auth.Infrastructure.Database.Model;

public class DbRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual DbRole? Role { get; init; }
}