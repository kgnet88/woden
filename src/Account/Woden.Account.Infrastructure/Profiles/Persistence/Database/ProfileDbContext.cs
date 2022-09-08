namespace KgNet88.Woden.Account.Infrastructure.Profiles.Persistence.Database;

public class ProfileDbContext : DbContext
{
    public DbSet<DbUserProfile> Profiles => this.Set<DbUserProfile>();

    public ProfileDbContext(DbContextOptions<ProfileDbContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.HasDefaultSchema("account");
    }
}