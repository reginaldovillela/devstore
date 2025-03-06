using SalesApi.Application.Sales.Models.Result;
using SalesApi.Domain.Sales.AggregatesModel;

namespace SalesApi.Application.Sales.Queries;

public class GetSaleByIdQueryHandler(ILogger<GetSaleByIdQueryHandler> logger,
                                     IMapper mapper,
                                     ISalesRepository salesRepository) 
    : IRequestHandler<GetSaleByIdQuery, Sale?>
{
    public async Task<Sale?> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await salesRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale is null)
        {
            logger.LogWarning("Sale order by @{Id} was not found", request.Id);
            return null;
        }

        return mapper.Map<SaleEntity, Sale>(sale);
    }
}
