using SalesApi.Application.Sales.Models.Request;
using SalesApi.Application.Sales.Models.Result;
using SalesApi.Domain.Sales.AggregatesModel;

namespace SalesApi.Application.Sales.Commands;

public class CreateSaleCommandHandler(ILogger<CreateSaleCommandHandler> logger,
                                      IMapper mapper,
                                      ISalesRepository salesRepository) 
    : IRequestHandler<CreateSaleCommand, Sale>
{
    public async Task<Sale> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await CreateAndInsertNewSaleAsync(request, cancellationToken);

        return mapper.Map<SaleEntity, Sale>(sale);
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

    private SaleEntity CreateNewSaleObject(CreateSaleCommand newSaleRequest)
    {
        var newSale = mapper.Map<CreateSaleCommand, SaleEntity>(newSaleRequest);

        foreach (var saleItemRequest in newSaleRequest.Items)
            newSale.AddSaleItem(
                mapper.Map<Item, SaleItemEntity>(saleItemRequest)
            );

        return newSale;
    }
}
