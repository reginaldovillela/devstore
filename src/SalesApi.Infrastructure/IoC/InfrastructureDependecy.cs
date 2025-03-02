using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesApi.Domain.Products.AggregatesModel;
using SalesApi.Infrastructure.Repositories;

namespace SalesApi.Infrastructure.IoC;

public static class InfrastructureDependecy
{
    public static void RegisterInfrastructureDependencies(this IServiceCollection services,
                                                               IConfiguration configuration)
    {
        services.AddDbContext<SalesContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("SalesConnection"));
        });

        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<SalesContext>();
    }
}

