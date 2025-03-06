using SalesApi.Application.Products.Models.Result;
using SalesApi.Domain.Products.AggregatesModel;

namespace SalesApi.Application.Products.Queries;

public class GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger,
                                     IMapper mapper,
                                     IProductsRepository productsRepository) : IRequestHandler<GetProductsQuery, Product[]>
{
    public async Task<Product[]> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productsRepository.GetAllAsync(cancellationToken);

        logger.LogInformation("Query did successful. Amount of {@count} records found", products.Count);

        return mapper.Map<ProductEntity[], Product[]>([.. products]);
    }
}
