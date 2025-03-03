using MediatR;
using SalesApi.Application.Products.Models.Result;
using SalesApi.Domain.Products.AggregatesModel;
using System.Data;

namespace SalesApi.Application.Products.Commands;

public class CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger,
                                         IProductsRepository productsRepository) : IRequestHandler<CreateProductCommand, Product>
{
    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await CheckIfProductAlreadyExistsAsync(request, cancellationToken);

        var product = await CreateAndInsertNewProductAsync(request, cancellationToken);

        return new Product(product.EntityId,
                           product.Title,
                           product.Description,
                           product.Price,
                           product.Category,
                           product.Image);
    }

    private async Task CheckIfProductAlreadyExistsAsync(CreateProductCommand newProductRequest, CancellationToken cancellationToken)
    {
        var product = await productsRepository.FindByTitleAsync(newProductRequest.Title, cancellationToken);

        if (product is not null)
        {
            logger.LogError("Product already exists by title: {@Title}", newProductRequest.Title);
            throw new ConstraintException($"Product already exists by title: {newProductRequest.Title}");
        }
    }

    private async Task<ProductEntity> CreateAndInsertNewProductAsync(CreateProductCommand newProductRequest, CancellationToken cancellationToken)
    {
        var newProduct = new ProductEntity(newProductRequest.Title,
                                           newProductRequest.Description,
                                           newProductRequest.Price,
                                           newProductRequest.Category,
                                           newProductRequest.Image);

        var success = await productsRepository.InsertAsync(newProduct, cancellationToken);

        if (!success)
        {
            logger.LogError("It wasn't to insert the product: {@Title}", newProduct.Title);
            throw new InvalidOperationException($"It wasn't to insert the product: {newProduct.Title}");
        }

        _ = await productsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created a new product: {@Product}", newProduct);

        return newProduct;
    }
}