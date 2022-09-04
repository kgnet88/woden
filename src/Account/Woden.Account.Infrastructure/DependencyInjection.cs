namespace KgNet88.Woden.Account.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        _ = services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        _ = services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        _ = services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        _ = services.AddDbContext<AuthDbContext>(options =>
        {
            _ = options.UseNpgsql(configuration.GetConnectionString("WodenDb")!);
            _ = options.UseLazyLoadingProxies();
        });

        _ = services
                .AddIdentity<DbUser, DbRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

        _ = services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.RequireUniqueEmail = false;
        });

        _ = services.AddAuthenticationJWTBearer(configuration["JwtSettings:Secret"] ?? "TokenSigningKeyAVeryDarkSecretString");

        _ = services.AddScoped<IAuthRepository, AuthRepository>();

        return services;
    }
}
