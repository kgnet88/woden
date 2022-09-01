namespace Goedde88.Woden.Account.Api.Auth.Infrastructure.Database.Model;

public class DbUserLogin : IdentityUserLogin<Guid>
{
    public virtual DbUser? User { get; init; }
}