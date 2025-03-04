using SalesApi.Domain.Products.AggregatesModel;
using SalesApi.Extensions;

namespace SalesApi.Services;

public class GetProductByIdConsumer(ILogger<GetProductByIdConsumer> logger,
                                    IProductsRepository productsRepository)
    : IConsumer<GetProductByIdRequest>
{
    public async Task Consume(ConsumeContext<GetProductByIdRequest> context)
    {
        try
        {
            var productId = context.Message.Id;

            var product = await productsRepository.GetByIdAsync(productId, new CancellationToken());

            if (product is null)
            {
                logger.LogWarning("Product by @{Id} was not found", productId);
                await context.RespondAsync(
                    new GetProductByIdResponse(true, 
                                               $"Product by {productId} was not found", 
                                               null));
            }
            else
            {
                await context.RespondAsync(
                    new GetProductByIdResponse(false, 
                                               string.Empty, 
                                               product));
            }
        }
        catch (Exception ex)
        {
            await context.RespondAsync(
                new GetProductByIdResponse(true, 
                                           ex.ReadAll(),
                                           null));
        }
    }
}
