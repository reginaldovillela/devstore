namespace SalesApi.Extensions;

internal static class ServiceExtensions
{
    public static void AddDefaultServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddEndpointsApiExplorer();

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
    }
}

