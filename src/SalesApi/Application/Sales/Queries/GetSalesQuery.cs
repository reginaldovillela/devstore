using SalesApi.Application.Sales.Models.Result;

namespace SalesApi.Application.Sales.Queries;

public record GetSalesQuery : IRequest<Sale[]>;
