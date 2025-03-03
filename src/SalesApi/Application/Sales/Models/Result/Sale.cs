namespace SalesApi.Application.Sales.Models.Result;

public record Sale(Guid Id, 
                   string SaleNumber, 
                   DateTime SaleDate, 
                   Guid CustomerId, 
                   Guid BranchId, 
                   decimal TotalAmount, 
                   bool Cancelled, 
                   ICollection<SaleItem> Items);
