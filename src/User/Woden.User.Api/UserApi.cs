namespace Goedde88.Woden.User.Api;

public static class UserApi
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        _ = builder.Services.AddDbContext<AuthDbContext>(
    options =>
    {
        _ = options.UseNpgsql(builder.Configuration.GetConnectionString("WodenDb")!);
        _ = options.UseLazyLoadingProxies();
    });

        _ = builder.Services.AddIdentity<DbUser, DbRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        _ = app.UseAuthentication();
        _ = app.UseAuthorization();

        app.Run();
    }
}