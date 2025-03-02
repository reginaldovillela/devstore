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
            logger.LogInformation("Já existe um produto com o título: {@Title}", newProductRequest.Title);
            throw new ConstraintException($"Já existe um produto com o título: {newProductRequest.Title}");
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
            logger.LogInformation("Não foi possível incluir o produto");
            throw new InvalidOperationException("Não foi possível incluir o produto");
        }

        _ = await productsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Criando o registro do produto: {@Product}", newProduct);

        return newProduct;
    }

}

