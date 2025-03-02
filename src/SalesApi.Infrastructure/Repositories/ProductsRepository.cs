using SalesApi.Domain.Products.AggregatesModel;

namespace SalesApi.Infrastructure.Repositories;

public class ProductsRepository(SalesContext context)
    : IProductsRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<ICollection<ProductEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await context
                                .Products
                                .AsNoTracking()
                                .ToListAsync(cancellationToken);

        return products;
    }

    public async Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await context
                                .Products
                                .AsNoTracking()
                                .Where(p => p.EntityId == id)
                                .SingleOrDefaultAsync(cancellationToken);

        return product;
    }
}

