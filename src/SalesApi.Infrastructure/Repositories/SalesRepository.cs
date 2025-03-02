using SalesApi.Domain.Sales.AggregatesModel;
using SalesApi.Domain.SeedWork.Interfaces;

namespace SalesApi.Infrastructure.Repositories;

public class SalesRepository : ISalesRepository
{
    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<SaleEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SaleEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

