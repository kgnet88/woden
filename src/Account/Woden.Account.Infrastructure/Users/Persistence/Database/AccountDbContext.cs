namespace KgNet88.Woden.Account.Infrastructure.Users.Persistence.Database;

/// <summary>
/// The database context for the BC account.
/// </summary>
public class AccountDbContext
    : IdentityDbContext<
        DbUser, DbRole, Guid,
        DbUserClaim, DbUserRole, DbUserLogin,
        DbRoleClaim, DbUserToken>
{
    /// <summary>
    /// Initializes a new instance of the db context.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
    public AccountDbContext(DbContextOptions<AccountDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Configures the schema needed for the identity framework.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.HasDefaultSchema("account");

        _ = modelBuilder.Entity<DbUser>(b =>
        {
            _ = b.ToTable("Users");

            // Each User can have many UserClaims
            _ = b.HasMany(e => e.Claims)
                .WithOne(e => e.User)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            _ = b.HasMany(e => e.Logins)
                .WithOne(e => e.User)
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            _ = b.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            _ = b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        _ = modelBuilder.Entity<DbRole>(b =>
        {
            _ = b.ToTable("Roles");

            // Each Role can have many entries in the UserRole join table
            _ = b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // Each Role can have many associated RoleClaims
            _ = b.HasMany(e => e.RoleClaims)
                .WithOne(e => e.Role)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();
        });

        _ = modelBuilder.Entity<DbUserClaim>(b => b.ToTable("UserClaims"));
        _ = modelBuilder.Entity<DbUserLogin>(b => b.ToTable("UserLogins"));
        _ = modelBuilder.Entity<DbRoleClaim>(b => b.ToTable("RoleClaims"));
        _ = modelBuilder.Entity<DbUserRole>(b => b.ToTable("UserRoles"));
        _ = modelBuilder.Entity<DbUserToken>(b => b.ToTable("UserTokens"));
    }
}