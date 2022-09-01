namespace KgNet88.Woden.Account.Api;

public static class AccountService
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

        _ = builder.Services.Configure<IdentityOptions>(options =>
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

        _ = builder.Services.AddFastEndpoints();
        _ = builder.Services.AddAuthenticationJWTBearer(builder.Configuration["JwtToken:Secret"] ?? "TokenSigningKeyAVeryDarkSecretString");

        _ = builder.Services.AddSwaggerDoc(settings =>
        {
            settings.Title = "Woden Auth API";
            settings.Version = "v1";
        },
        serializer =>
        {
            serializer.PropertyNamingPolicy = null;
            serializer.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        }, shortSchemaNames: true);

        _ = builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        _ = builder.Services.AddScoped<IAuthService, AuthService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        _ = app.UseMiddleware<ValidationExceptionMiddleware>();

        _ = app.UseAuthentication();
        _ = app.UseAuthorization();

        _ = app.UseFastEndpoints(c =>
        {
            c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            _ = c.Serializer.Options.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            c.Endpoints.RoutePrefix = "api";
            c.Endpoints.ShortNames = true;

            c.Errors.ResponseBuilder = (failures, _) => new
            {
                Errors = failures.ConvertAll(y => y.ErrorMessage)
            };
        });

        _ = app.UseOpenApi();
        _ = app.UseSwaggerUi3(s => s.ConfigureDefaults());

        app.Run();
    }
}