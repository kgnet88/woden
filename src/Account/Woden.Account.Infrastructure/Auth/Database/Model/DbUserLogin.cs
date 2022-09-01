namespace KgNet88.Woden.Account.Infrastructure.Auth.Database.Model;

public class DbUserLogin : IdentityUserLogin<Guid>
{
    public virtual DbUser? User { get; init; }
}