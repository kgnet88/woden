namespace KgNet88.Woden.Account.Infrastructure.Auth.Persistence.Database.Model;

public class DbUserClaim : IdentityUserClaim<Guid>
{
    public virtual DbUser? User { get; init; }
}
