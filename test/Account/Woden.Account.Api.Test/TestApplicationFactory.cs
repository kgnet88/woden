namespace KgNet88.Woden.Account.Api.Test;

public sealed class TestApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var config = this.InitConfiguration();

        _ = builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<AuthDbContext>));

            _ = services.Remove(descriptor!);

            _ = services.AddDbContext<AuthDbContext>(options =>
            {
                _ = options.UseNpgsql(config.GetConnectionString("TestDb")!);
                _ = options.UseLazyLoadingProxies();
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AuthDbContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<TestApplicationFactory<TStartup>>>();

                _ = db.Database.EnsureDeleted();

                db.Database.Migrate();
            }
        });
    }

    public async Task ResetDatabaseAsync()
    {
        using (var scope = this.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
            _ = await db.Database.EnsureDeletedAsync();

            db.Database.Migrate();
        }
    }

    public override async ValueTask DisposeAsync()
    {
        using (var scope = this.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
            _ = await db.Database.EnsureDeletedAsync();
        }

        await base.DisposeAsync();
    }

    public IConfiguration InitConfiguration()
    {
        return new ConfigurationBuilder()
           .AddJsonFile(Path.Combine(this.ContentRootPath(), "testconfig.json"))
           .Build();
    }

    public string ContentRootPath()
    {
        string rootDirectory = AppDomain.CurrentDomain.BaseDirectory;
        if (rootDirectory.Contains("bin"))
        {
            rootDirectory = rootDirectory[..rootDirectory.IndexOf("bin")];
        }
        return rootDirectory;
    }
}