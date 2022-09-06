namespace KgNet88.Woden.Account.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        _ = services.AddAuth(configuration);
        _ = services.AddProfileDb(configuration);

        _ = services.AddScoped<IAuthRepository, AuthRepository>();
        _ = services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        _ = services.AddSingleton(Options.Create(jwtSettings));
        _ = services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

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
            options.User.RequireUniqueEmail = true;
        });

        _ = services.AddAuthenticationJWTBearer(jwtSettings.Secret ?? "TokenSigningKeyAVeryDarkSecretString");

        return services;
    }

    public static IServiceCollection AddProfileDb(this IServiceCollection services, ConfigurationManager configuration)
    {
        _ = services.AddDbContext<ProfileDbContext>(options =>
        {
            _ = options.UseNpgsql(configuration.GetConnectionString("WodenDb")!);
            _ = options.UseLazyLoadingProxies();
        });

        return services;
    }
}
