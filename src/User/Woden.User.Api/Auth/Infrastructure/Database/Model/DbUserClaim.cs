namespace Goedde88.Woden.User.Api.Auth.Infrastructure.Database.Model;

public class DbUserClaim : IdentityUserClaim<Guid>
{
    public virtual DbUser? User { get; init; }
}
