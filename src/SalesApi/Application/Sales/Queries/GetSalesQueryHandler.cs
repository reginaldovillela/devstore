using SalesApi.Application.Sales.Models.Result;
using SalesApi.Domain.Sales.AggregatesModel;

namespace SalesApi.Application.Sales.Queries;

public class GetSalesQueryHandler(ILogger<GetSalesQueryHandler> logger,
                                  IMapper mapper,
                                  ISalesRepository salesRepository)
    : IRequestHandler<GetSalesQuery, Sale[]>
{
    public async Task<Sale[]> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        var sales = await salesRepository.GetAllAsync(cancellationToken);

        logger.LogInformation("Query did successful. Amount of {@count} records found", sales.Count);

        return mapper.Map<SaleEntity[], Sale[]>([.. sales]);
    }
}
