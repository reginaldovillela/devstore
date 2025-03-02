using SalesApi.Domain.Products.AggregatesModel;
using SalesApi.Domain.Sales.AggregatesModel;

namespace SalesApi.Infrastructure;

public class SalesContext
    : DbContext, IUnitOfWork
{
    public DbSet<ProductEntity> Products { get; set; }

    //public DbSet<CategoryEntity> Categories { get; set; }

    //public DbSet<SaleEntity> Sales { get; set; }

    //public DbSet<ItemSaleEntity> ItemsSale { get; set; }

    public SalesContext(DbContextOptions<SalesContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        _ = await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}

