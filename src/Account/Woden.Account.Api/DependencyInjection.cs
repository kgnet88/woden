namespace KgNet88.Woden.Account.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        _ = services.AddControllers();
        _ = services.AddSingleton<ProblemDetailsFactory, CommonProblemDetailsFactory>();
        _ = services.AddMappings();
        _ = services.AddEndpoints();

        return services;
    }

    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        _ = services.AddFastEndpoints();

        _ = services.AddSwaggerDoc(settings =>
        {
            settings.Title = "Woden Auth API";
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