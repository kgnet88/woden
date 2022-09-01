namespace Goedde88.Woden.Account.Api.Auth.Infrastructure.Database.Model;

public class DbUserToken : IdentityUserToken<Guid>
{
    public virtual DbUser? User { get; init; }
}
