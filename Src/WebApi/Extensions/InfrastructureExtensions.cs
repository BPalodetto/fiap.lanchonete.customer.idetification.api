using Core.Services.Interfaces;
using Core.Services;
using Core.Repositories;
using Infrastructure.Repositories;
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
                client.BaseAddress = new Uri("");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

        return services;
    }
}
