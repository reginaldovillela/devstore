using SalesApi.Application.Sales.Models.Result;

namespace SalesApi.Application.Sales.Queries;

public record GetSaleByIdQuery(
    [property: JsonIgnore] Guid Id
) : IRequest<Sale?>;
