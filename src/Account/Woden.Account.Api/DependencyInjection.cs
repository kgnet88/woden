namespace KgNet88.Woden.Account.Api;

/// <summary>
/// Extension methods for injected services during startup.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Extension method which adds injected services for the presentation layer to the account service.
    /// </summary>
    /// <param name="services">the account services existing service collection.</param>
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        _ = services.AddEndpoints();

        return services;
    }

    /// <summary>
    /// Extension method which adds injected services for the fast endpoint library.
    /// </summary>
    /// <param name="services">the account services existing service collection.</param>
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        _ = services.AddFastEndpoints();

        _ = services.AddSwaggerDoc(settings =>
        {
            settings.Title = "Woden Account API";
            settings.Version = "v1";
        },
        serializer =>
        {
            serializer.PropertyNamingPolicy = null;
            serializer.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        }, shortSchemaNames: true);

        return services;
    }
}