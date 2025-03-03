namespace SalesApi.Application.Sales.Commands;

public record CancelSaleCommand(
    [property: JsonIgnore] Guid Id
) : IRequest<bool>;
