using SalesApi.Domain.Sales.AggregatesModel;

namespace SalesApi.Application.Sales.Commands;

public class CancelSaleCommandHandler(ILogger<CancelSaleCommandHandler> logger,
                                      ISalesRepository salesRepository) 
    : IRequestHandler<CancelSaleCommand, bool>
{
    public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await salesRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale is null)
        {
            logger.LogWarning("Sale order by @{Id} was not found", request.Id);
            throw new InvalidOperationException($"Sale order by {request.Id} was not found");
        }

        return await CancelSale(sale, cancellationToken);
    }

    private async Task<bool> CancelSale(SaleEntity sale, CancellationToken cancellationToken)
    {
        sale.CancelSale();

        var success = await salesRepository.UpdateAsync(sale, cancellationToken);

        _ = await salesRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return success;
    }
}
