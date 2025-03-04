using SalesApi.Services;

namespace SalesApi.Extensions;

internal static class ServiceExtensions
{
    public static void AddDefaultServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.Configure<ApiBehaviorOptions>(config =>
        {
            config.SuppressInferBindingSourcesForParameters = true;
        });
    }

    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        var config = builder.Configuration;

        // mediatr
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<Program>();
        });

        // automapper
        services.AddAutoMapper(typeof(Program));

        services.AddMassTransit(bus =>
        {
            bus.SetKebabCaseEndpointNameFormatter();
            bus.AddConsumer<GetProductByIdConsumer>().Endpoint(
                e => e.Name = "get-product-by-id");
            bus.AddRequestClient<GetProductByIdRequest>(
                new Uri("exchange:get-product-by-id"));

            bus.UsingInMemory((context, x) =>
            {
                x.ConfigureEndpoints(context);
            });
        });
    }
}

