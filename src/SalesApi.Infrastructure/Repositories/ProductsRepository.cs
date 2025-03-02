using SalesApi.Domain.Products.AggregatesModel;
using SalesApi.Domain.SeedWork.Interfaces;

namespace SalesApi.Infrastructure.Repositories;

public class ProductsRepository : IProductsRepository
{
    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public Task<ICollection<ProductEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

