using SalesApi.Application.Sales.Commands;
using SalesApi.Application.Sales.Models.Request;
using SalesApi.Application.Sales.Models.Result;
using SalesApi.Domain.Sales.AggregatesModel;

namespace SalesApi.Application.Sales.Maps;

public class MappingSalesProfile : Profile
{
    public MappingSalesProfile()
    {
        CreateMap<CreateSaleCommand, SaleEntity>()
            .ForCtorParam("saleNumber", o => o.MapFrom(f => f.SaleNumber))
            .ForCtorParam("saleDate", o => o.MapFrom(f => f.SaleDate))
            .ForCtorParam("customerId", o => o.MapFrom(f => f.CustomerId))
            .ForCtorParam("branchId", o => o.MapFrom(f => f.BranchId));

        CreateMap<Item, SaleItemEntity>()
            .ForCtorParam("productId", o => o.MapFrom(f => f.ProductId))
            .ForCtorParam("quantity", o => o.MapFrom(f => f.Quantity))
            .ForCtorParam("unitPrice", o => o.MapFrom(f => f.UnitPrice));

        CreateMap<SaleEntity, Sale>()
            .ForCtorParam("Id", o => o.MapFrom(f => f.EntityId))
            .ForCtorParam("SaleNumber", o => o.MapFrom(f => f.SaleNumber))
            .ForCtorParam("SaleDate", o => o.MapFrom(f => f.SaleDate))
            .ForCtorParam("CustomerId", o => o.MapFrom(f => f.CustomerId))
            .ForCtorParam("BranchId", o => o.MapFrom(f => f.BranchId))
            .ForCtorParam("TotalAmount", o => o.MapFrom(f => f.Total))
            .ForCtorParam("Cancelled", o => o.MapFrom(f => f.IsCancelled))
            .ForCtorParam("Items", o => o.MapFrom(f => f.SaleItems));

        CreateMap<SaleItemEntity, SaleItem>()
            .ForCtorParam("Id", o => o.MapFrom(f => f.EntityId))
            .ForCtorParam("ProductId", o => o.MapFrom(f => f.ProductId))
            .ForCtorParam("Quantity", o => o.MapFrom(f => f.Quantity))
            .ForCtorParam("UnitPrice", o => o.MapFrom(f => f.UnitPrice))
            .ForCtorParam("Discount", o => o.MapFrom(f => f.TotalDiscount))
            .ForCtorParam("Total", o => o.MapFrom(f => f.Total))
            .ForCtorParam("SaleId", o => o.MapFrom(f => f.SaleId))
            .ForCtorParam("IsCancelled", o => o.MapFrom(f => f.IsCancelled));
    }
}
