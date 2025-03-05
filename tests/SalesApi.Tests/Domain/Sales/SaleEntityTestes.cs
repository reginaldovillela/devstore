using SalesApi.Domain.Sales.AggregatesModel;

namespace SalesApi.Tests.Domain.Sales;

public class SaleEntityTestes
{
    public static TheoryData<string, DateTime, Guid, Guid> InvalidCases = new()
    {
        { "1234", DateTime.Now.AddDays(1), Guid.NewGuid(), Guid.NewGuid() }
    };

    public static TheoryData<string, DateTime, Guid, Guid> ValidCases = new()
    {
        { "1234", DateTime.Now, Guid.NewGuid(), Guid.NewGuid() }
    };

    [Theory(DisplayName = "Try to create a sale with invalid values")]
    [MemberData(nameof(InvalidCases))]
    [Trait("Domain", "Sale")]
    public void TryToCreateSaleWithInvalidValues_Failure(string saleNumber, DateTime saleDate, Guid customerId, Guid branchId)
    {
        Assert.Throws<InvalidOperationException>(() => new SaleEntity(saleNumber, saleDate, customerId, branchId));
    }

    [Theory(DisplayName = "Try to create a sale with valid values")]
    [MemberData(nameof(ValidCases))]
    [Trait("Domain", "Sale")]
    public void TryToCreateSaleWithValidValues_Success(string saleNumber, DateTime saleDate, Guid customerId, Guid branchId)
    {
        var sale = new SaleEntity(saleNumber, saleDate, customerId, branchId);

        Assert.NotNull(sale);
    }
}
