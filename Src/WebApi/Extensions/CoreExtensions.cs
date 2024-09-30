using Core.Services;
using Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Extensions;

public static class CoreExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return
            services
                .AddServices();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<ICustomerAuthenticationService, CustomerAuthenticationService>()
            .AddScoped<IOrderService, OrderService>();

    }
}
