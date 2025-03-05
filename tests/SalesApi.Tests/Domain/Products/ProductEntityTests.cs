using SalesApi.Domain.Products.AggregatesModel;

namespace SalesApi.Tests.Domain.Products;

public class ProductEntityTests
{
    [Theory(DisplayName = "Try to create a product with invalid values")]
    [InlineData("title", "description", 0, "category", "image")]
    [InlineData("title more than 10", "description", 0, "category", "image")]
    [InlineData("title", "description more than 15", 0, "category", "image")]
    [InlineData("title", "description", 1, "category", "image")]
    [Trait("Domain", "Product")]
    public void TryToCreateProductWithInvalidValues_Failure(string title, string description, decimal price, string category, string image)
    {
        Assert.Throws<InvalidOperationException>(() => new ProductEntity(title, description, price, category, image));
    }

    [Theory(DisplayName = "Try to create a product with valid values")]
    [InlineData("title more than 10", "description more than 15", 1, "category", "image")]
    [Trait("Domain", "Product")]
    public void TryToCreateProductWithValidValues_Success(string title, string description, decimal price, string category, string image)
    {
        var product = new ProductEntity(title, description, price, category, image);

        Assert.NotNull(product);
    }
}
