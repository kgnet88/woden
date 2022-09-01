namespace KgNet88.Woden.Account.Api.Auth.Infrastructure.Database;

public class AuthDbContext
    : IdentityDbContext<
        DbUser, DbRole, Guid,
        DbUserClaim, DbUserRole, DbUserLogin,
        DbRoleClaim, DbUserToken>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.HasDefaultSchema("auth");

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