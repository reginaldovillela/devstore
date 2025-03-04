using SalesApi.Application.Sales.Models.Request;
using SalesApi.Application.Sales.Models.Result;
using SalesApi.Domain.Sales.AggregatesModel;
using SalesApi.Services;

namespace SalesApi.Application.Sales.Commands;

public class CreateSaleCommandHandler(ILogger<CreateSaleCommandHandler> logger,
                                      IMapper mapper,
                                      ISalesRepository salesRepository,
                                      IRequestClient<GetProductByIdRequest> productConsumer)
    : IRequestHandler<CreateSaleCommand, Sale>
{
    public async Task<Sale> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await CreateAndInsertNewSaleAsync(request, cancellationToken);

        return mapper.Map<SaleEntity, Sale>(sale);
    }

    private async Task<SaleEntity> CreateAndInsertNewSaleAsync(CreateSaleCommand newSaleRequest, CancellationToken cancellationToken)
    {
        var newSale = await CreateNewSaleObject(newSaleRequest);

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

    private async Task<SaleEntity> CreateNewSaleObject(CreateSaleCommand newSaleRequest)
    {
        var newSale = mapper.Map<CreateSaleCommand, SaleEntity>(newSaleRequest);

        foreach (var saleItemRequest in newSaleRequest.Items)
            newSale.AddSaleItem(
                await ValidateAndCreateSaleItemAsync(saleItemRequest)
            );

        return newSale;
    }

    private async Task<SaleItemEntity> ValidateAndCreateSaleItemAsync(Item item)
    {
        var request = new GetProductByIdRequest(item.ProductId);
        var response = (await productConsumer.GetResponse<GetProductByIdResponse>(request)).Message;

        if (response.HasError)
        {
            logger.LogError("It was not possible to add product @{Id} > @{Error}", item.ProductId, response.ErrorMessage);
            throw new InvalidOperationException($"It was not possible to add product {item.ProductId} > {response.ErrorMessage}");
        }

        var product = response.Product!;

        return new SaleItemEntity(product.EntityId,
                                  item.Quantity,
                                  product.Price);
    }
}
