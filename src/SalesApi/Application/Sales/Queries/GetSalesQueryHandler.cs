using SalesApi.Application.Sales.Models.Result;
using SalesApi.Domain.Sales.AggregatesModel;

namespace SalesApi.Application.Sales.Queries;

public class GetSalesQueryHandler(ILogger<GetSalesQueryHandler> logger,
                                  ISalesRepository salesRepository)
    : IRequestHandler<GetSalesQuery, Sale[]>
{
    public async Task<Sale[]> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        var sales = await salesRepository.GetAllAsync(cancellationToken);

        logger.LogInformation("Query did successful. Amount of {@count} records found", sales.Count);

        return [.. sales.Select(s=> new Sale(s.EntityId,
                        s.SaleNumber.ToString(),
                        s.SaleDate,
                        s.CustomerId,
                        s.BranchId,
                        s.Total,
                        s.IsCancelled,
                        [.. s.SaleItems.Select(i=> new SaleItem(i.EntityId,
                                                                   i.ProductId,
                                                                   i.Quantity,
                                                                   i.UnitPrice,
                                                                   i.TotalDiscount,
                                                                   i.Total,
                                                                   i.SaleId,
                                                                   i.IsCancelled))]))];
    }
}
