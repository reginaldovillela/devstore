namespace SalesApi.Application.Sales.Models.Request;

public record Item(Guid ProductId, 
                   ushort Quantity, 
                   decimal UnitPrice, 
                   decimal TotalPrice);

