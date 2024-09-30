using Core.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Utils;
using System.Net.Http.Headers;

namespace WebApi.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return
         services
             .AddRepositories();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        
        services
           .AddHttpClient<ICustomerRepository, CustomerRepository>("CustomerClient", client =>
            {
                client.BaseAddress = new Uri(InfrastructureEnviromentVariables.FiapLanchonetApi);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

        services
           .AddHttpClient<IOrderRepository, OrderRepository>("OrderClient", client =>
           {
               client.BaseAddress = new Uri(InfrastructureEnviromentVariables.FiapLanchonetApi);
               client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           });

        return services;
    }
}
