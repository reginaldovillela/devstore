using SalesApi.Application.Sales.Models.Request;
using SalesApi.Application.Sales.Models.Result;

namespace SalesApi.Application.Sales.Commands;

public class CreateSaleCommand : IRequest<Sale>
{
    public string SaleNumber { get; init; } = string.Empty;

    public DateTime SaleDate { get; init; }

    public Guid CustomerId { get; init; }

    public Guid BranchId { get; init; }

    public ICollection<Item> Items { get; init; } = null!;
}