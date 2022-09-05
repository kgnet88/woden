namespace KgNet88.Woden.Account.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        _ = services.AddMediatR(typeof(DependencyInjection).Assembly);

        _ = services.AddScoped(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));

        _ = services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
