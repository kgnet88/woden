namespace KgNet88.Woden.Account.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        _ = services.AddSingleton<ProblemDetailsFactory, CommonProblemDetailsFactory>();
        _ = services.AddMappings();
        return services;
    }
}
