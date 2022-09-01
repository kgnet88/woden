namespace Goedde88.Woden.Account.Api.Auth.Infrastructure.Database.Model;

public class DbRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual DbRole? Role { get; init; }
}