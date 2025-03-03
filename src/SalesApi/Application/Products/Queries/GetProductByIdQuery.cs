using SalesApi.Application.Products.Models.Result;

namespace SalesApi.Application.Products.Queries;

public record GetProductByIdQuery(
    [property: JsonIgnore] Guid Id
) : IRequest<Product?>;