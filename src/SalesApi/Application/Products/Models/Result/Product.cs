namespace SalesApi.Application.Products.Models.Result;

public record Product(Guid Id,
                      string Title,
                      string Description,
                      decimal Price,
                      string Category,
                      string Image);
