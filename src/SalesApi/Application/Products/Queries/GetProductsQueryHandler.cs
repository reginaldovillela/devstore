using MediatR;
using SalesApi.Application.Products.Models.Result;
using SalesApi.Domain.Products.AggregatesModel;

namespace SalesApi.Application.Products.Queries;

public class GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger,
                                     IProductsRepository productsRepository) : IRequestHandler<GetProductsQuery, Product[]>
{
    public async Task<Product[]> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productsRepository.GetAllAsync(cancellationToken);

        logger.LogInformation("Consulta concluída. Total de {@count} encontrados", products.Count);

        return [.. products.Select(p => new Product(p.EntityId,
                                                    p.Title,
                                                    p.Description,
                                                    p.Price,
                                                    p.Category,
                                                    p.Image))];
    }
}
