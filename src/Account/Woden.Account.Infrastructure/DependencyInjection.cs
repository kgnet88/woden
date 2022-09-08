namespace KgNet88.Woden.Account.Infrastructure;

/// <summary>
/// Extension methods for injected services during startup.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Extension method which adds injected services for the infrastructur layer to the account service.
    /// </summary>
    /// <param name="services">the account services existing service collection.</param>
    /// <param name="configuration">the application configuration.</param>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        _ = services.AddAuth(configuration);
        _ = services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    /// <summary>
    /// Extension method for injected services and neccessary setup for authentication.
    /// </summary>
    /// <param name="services">the account services existing service collection.</param>
    /// <param name="configuration">the application configuration.</param>
    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        _ = services.AddSingleton(Options.Create(jwtSettings));
        _ = services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        _ = services.AddDbContext<AccountDbContext>(options =>
        {
            _ = options.UseNpgsql(configuration.GetConnectionString("WodenDb")!);
            _ = options.UseLazyLoadingProxies();
        });

        _ = services
                .AddIdentity<DbUser, DbRole>()
                .AddEntityFrameworkStores<AccountDbContext>()
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
            options.User.RequireUniqueEmail = true;
        });

        _ = services.AddAuthenticationJWTBearer(jwtSettings.Secret ?? "TokenSigningKeyAVeryDarkSecretString");

        return services;
    }
}