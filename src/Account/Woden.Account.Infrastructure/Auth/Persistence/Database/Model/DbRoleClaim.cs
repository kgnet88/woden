namespace KgNet88.Woden.Account.Infrastructure.Auth.Persistence.Database.Model;

public class DbRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual DbRole? Role { get; init; }
}