namespace SalesApi.Application.Sales.Models.Result;

public record SaleItem(Guid Id, 
                       Guid ProductId, 
                       ushort Quantity, 
                       decimal Discount, 
                       decimal Total, 
                       Guid SaleId, 
                       bool IsCancelled);
