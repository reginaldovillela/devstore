using SalesApi.Application.Products.Models.Result;
using SalesApi.Domain.Products.AggregatesModel;

namespace SalesApi.Application.Products.Queries;

public class GetProductByIdQueryHandler(ILogger<GetProductByIdQueryHandler> logger,
                                        IProductsRepository productsRepository)
    : IRequestHandler<GetProductByIdQuery, Product?>
{
    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productsRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            logger.LogWarning("Product by @{Id} was not found", request.Id);
            return null;
        }

        return new Product(product.EntityId,
                           product.Title,
                           product.Description,
                           product.Price,
                           product.Category,
                           product.Image);
    }
}
