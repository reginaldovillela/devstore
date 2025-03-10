﻿using SalesApi.Domain.Sales.AggregatesModel;

namespace SalesApi.Infrastructure.Repositories;

public class SalesRepository(SalesContext context)
    : ISalesRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<ICollection<SaleEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var sales = await context
                            .Sales
                            .Include(i => i.SaleItems)
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

        return sales;
    }

    public async Task<SaleEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var sale = await context
                            .Sales
                            .Include(i => i.SaleItems)
                            .AsNoTracking()
                            .Where(s => s.EntityId == id)
                            .SingleOrDefaultAsync(cancellationToken);

        return sale;
    }

    public async Task<bool> InsertAsync(SaleEntity sale, CancellationToken cancellationToken)
    {
        _ = await context.Sales.AddAsync(sale, cancellationToken);

        return true;
    }

    public async Task<bool> UpdateAsync(SaleEntity saleToUpdate, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            _ = context.Sales.Update(saleToUpdate);

            return true;
        }, cancellationToken);
    }
}

