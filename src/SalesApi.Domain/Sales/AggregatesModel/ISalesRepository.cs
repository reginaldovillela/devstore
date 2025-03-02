namespace SalesApi.Domain.Sales.AggregatesModel;

public interface ISalesRepository : IRepository<SaleEntity>
{
    Task<ICollection<SaleEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<SaleEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> UpdateAsync(SaleEntity saleToUpdate, CancellationToken cancellationToken);
}

