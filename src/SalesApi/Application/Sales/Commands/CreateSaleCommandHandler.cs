using SalesApi.Application.Sales.Models.Result;
using SalesApi.Domain.Sales.AggregatesModel;

namespace SalesApi.Application.Sales.Commands;

public class CreateSaleCommandHandler(ILogger<CreateSaleCommandHandler> logger,
                                      ISalesRepository salesRepository) 
    : IRequestHandler<CreateSaleCommand, Sale>
{
    public async Task<Sale> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await CreateAndInsertNewSaleAsync(request, cancellationToken);

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
                                                               i.Discount,
                                                               i.Total,
                                                               i.SaleId,
                                                               i.IsCancelled))]);
    }

    private async Task<SaleEntity> CreateAndInsertNewSaleAsync(CreateSaleCommand newSaleRequest, CancellationToken cancellationToken)
    {
        var newSale = CreateNewSaleObject(newSaleRequest);

        var success = await salesRepository.InsertAsync(newSale, cancellationToken);

        if (!success)
        {
            logger.LogInformation("It wasn't to insert a sale order.");
            throw new InvalidOperationException("It wasn't to insert a sale order.");
        }

        _ = await salesRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created a new sale order: {@Sale}", newSale);

        return newSale;
    }

    private static SaleEntity CreateNewSaleObject(CreateSaleCommand newSaleRequest)
    {
        var newSale = new SaleEntity(newSaleRequest.SaleNumber,
                                             newSaleRequest.SaleDate,
                                             newSaleRequest.CustomerId,
                                             newSaleRequest.BranchId);

        foreach (var saleItemRequest in newSaleRequest.Items)
        {
            var saleItem = new SaleItemEntity(saleItemRequest.ProductId,
                                              saleItemRequest.Quantity,
                                              saleItemRequest.UnitPrice);

            newSale.AddSaleItem(saleItem);
        }

        return newSale;
    }
}
