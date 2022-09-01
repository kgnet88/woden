namespace KgNet88.Woden.Account.Infrastructure.Auth.Database.Model;

public class DbUserRole : IdentityUserRole<Guid>
{
    public virtual DbUser? User { get; init; }
    public virtual DbRole? Role { get; init; }
}
