using SalesApi.Application.Products.Models.Result;

namespace SalesApi.Application.Products.Commands;

public class CreateProductCommand : IRequest<Product>
{
    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public decimal Price { get; init; }

    public string Category { get; init; } = string.Empty;

    public string Image { get; init; } = string.Empty;
}

