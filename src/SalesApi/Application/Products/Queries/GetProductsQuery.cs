using SalesApi.Application.Products.Models.Result;

namespace SalesApi.Application.Products.Queries;

public record GetProductsQuery : IRequest<Product[]>;