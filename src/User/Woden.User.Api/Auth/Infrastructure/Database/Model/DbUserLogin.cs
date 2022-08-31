namespace Goedde88.Woden.User.Api.Auth.Infrastructure.Database.Model;

public class DbUserLogin : IdentityUserLogin<Guid>
{
    public virtual DbUser? User { get; init; }
}