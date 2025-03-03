using SalesApi.Application.Products.Commands;
using SalesApi.Application.Products.Models.Result;
using SalesApi.Domain.Products.AggregatesModel;

namespace SalesApi.Application.Products.Maps;

public class MappingProductsProfile : Profile
{
    public MappingProductsProfile()
    {
        CreateMap<CreateProductCommand, ProductEntity>()
            .ForCtorParam("title", o => o.MapFrom(f => f.Title))
            .ForCtorParam("description", o => o.MapFrom(f => f.Description))
            .ForCtorParam("price", o => o.MapFrom(f => f.Price))
            .ForCtorParam("category", o => o.MapFrom(f => f.Category))
            .ForCtorParam("image", o => o.MapFrom(f => f.Image));

        CreateMap<ProductEntity, Product>()
            .ForCtorParam("Id", o => o.MapFrom(f => f.EntityId))
            .ForCtorParam("Title", o => o.MapFrom(f => f.Title))
            .ForCtorParam("Description", o => o.MapFrom(f => f.Description))
            .ForCtorParam("Price", o => o.MapFrom(f => f.Price))
            .ForCtorParam("Category", o => o.MapFrom(f => f.Category))
            .ForCtorParam("Image", o => o.MapFrom(f => f.Image));
    }
}
