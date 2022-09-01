namespace KgNet88.Woden.Account.Api.Auth.Infrastructure.Database.Model;

public class DbUserClaim : IdentityUserClaim<Guid>
{
    public virtual DbUser? User { get; init; }
}
