using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServicesInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Transient);
    }
}