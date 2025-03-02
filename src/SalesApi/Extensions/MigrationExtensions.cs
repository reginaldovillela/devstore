using Microsoft.EntityFrameworkCore;
using SalesApi.Infrastructure;

namespace SalesApi.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        using var orderContext = scope.ServiceProvider.GetRequiredService<SalesContext>();
        orderContext.Database.Migrate();
    }
}

