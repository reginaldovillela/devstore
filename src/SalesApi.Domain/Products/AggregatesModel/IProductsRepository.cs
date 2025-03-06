namespace SalesApi.Domain.Products.AggregatesModel;

public interface IProductsRepository : IRepository<ProductEntity>
{
    Task<ProductEntity?> FindByTitleAsync(string title, CancellationToken cancellationToken);

    Task<ICollection<ProductEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> InsertAsync(ProductEntity product, CancellationToken cancellationToken);
}

