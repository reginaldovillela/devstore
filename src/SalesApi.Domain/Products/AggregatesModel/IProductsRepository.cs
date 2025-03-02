namespace SalesApi.Domain.Products.AggregatesModel;

public interface IProductsRepository : IRepository<ProductEntity>
{
    Task<ICollection<ProductEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}

