namespace KgNet88.Woden.Account.Api.Auth.Mapping;

public static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        _ = config.Scan(Assembly.GetExecutingAssembly());

        _ = services.AddSingleton(config);
        _ = services.AddScoped<MapsterMapper.IMapper, ServiceMapper>();

        return services;
    }
}