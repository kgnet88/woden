using MediatR;

namespace KgNet88.Woden.Account.Application;
public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        _ = services.AddMediatR(typeof(DependencyInjection).Assembly);

        return services;
    }
}
