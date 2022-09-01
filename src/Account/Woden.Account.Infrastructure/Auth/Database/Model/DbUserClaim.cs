namespace KgNet88.Woden.Account.Infrastructure.Auth.Database.Model;

public class DbUserClaim : IdentityUserClaim<Guid>
{
    public virtual DbUser? User { get; init; }
}
