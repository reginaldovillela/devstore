using SalesApi.Application.Sales.Models.Result;
using SalesApi.Domain.Sales.AggregatesModel;

namespace SalesApi.Application.Sales.Queries;

public class GetSaleByIdQueryHandler(ILogger<GetSaleByIdQueryHandler> logger,
                                     ISalesRepository salesRepository) 
    : IRequestHandler<GetSaleByIdQuery, Sale?>
{
    public async Task<Sale?> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await salesRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale is null)
        {
            logger.LogWarning("Sale order by @{Id} was not found", request.Id);
            return null;
        }

        return new Sale(sale.EntityId,
                        sale.SaleNumber.ToString(),
                        sale.SaleDate,
                        sale.CustomerId,
                        sale.BranchId,
                        sale.Total,
                        sale.IsCancelled,
                        [.. sale.SaleItems.Select(i=> new SaleItem(i.EntityId,
                                                                   i.ProductId,
                                                                   i.Quantity,
                                                                   i.UnitPrice,
                                                                   i.TotalDiscount,
                                                                   i.Total,
                                                                   i.SaleId,
                                                                   i.IsCancelled))]);
    }
}
