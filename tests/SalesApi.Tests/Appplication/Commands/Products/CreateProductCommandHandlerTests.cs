using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using SalesApi.Application.Products.Commands;
using SalesApi.Application.Products.Maps;
using SalesApi.Application.Products.Models.Result;
using SalesApi.Domain.Products.AggregatesModel;
using SalesApi.Domain.SeedWork.Interfaces;

namespace SalesApi.Tests.Appplication.Commands.Products;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<ILogger<CreateProductCommandHandler>> _logger = new();
    private readonly IMapper _mapper;
    private readonly Mock<IProductsRepository> _productRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandHandlerTests()
    {
        var mockMapperProfile = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProductsProfile());
        });

        _mapper = mockMapperProfile.CreateMapper();

        _handler = new CreateProductCommandHandler(_logger.Object,
                                                   _mapper,
                                                   _productRepository.Object);
    }

    //[Fact(DisplayName = "Try to create a product with valid values using commands")]
    //[Trait("Application > Command", "Products")]
    //public async Task CreateAProductWithValidValues_Success()
    //{
    //    var command = new CreateProductCommand
    //    {
    //        Title = "Title of Test",
    //        Description = "Description of Test",
    //        Price = 10,
    //        Category = "Category Test",
    //        Image = "URL Image"
    //    };

    //    var productEntity = _mapper.Map<CreateProductCommand, ProductEntity>(command);

    //    var productExpected = new Product(productEntity.EntityId,
    //                                      productEntity.Title,
    //                                      productEntity.Description,
    //                                      productEntity.Price,
    //                                      productEntity.Category,
    //                                      productEntity.Image);

    //    _productRepository
    //        .Setup(repo => repo.InsertAsync(productEntity, CancellationToken.None))
    //        .ReturnsAsync(true);

    //    var response = await _handler.Handle(command, It.IsAny<CancellationToken>());

    //    Assert.Equal(productExpected, response);
    //}
}
