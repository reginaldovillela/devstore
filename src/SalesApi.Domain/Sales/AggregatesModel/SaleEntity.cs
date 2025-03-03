using System.Security.Cryptography;

namespace SalesApi.Domain.Sales.AggregatesModel;

[Table("sales")]
public class SaleEntity
    : Entity, IAggregateRoot
{
    public string SaleNumber { get; init; }

    public DateTime SaleDate { get; private set; }

    public Guid CustomerId { get; init; }

    public Guid BranchId { get; init; }

    public bool IsCancelled { get; private set; } = false;

    public decimal Total => CalculateTotalToPay();

    #region "ef requirements and relations"

    [InverseProperty("sale")]
    public ICollection<SaleItemEntity> SaleItems { get; private set; } = null!;

#pragma warning disable CS8618
    protected SaleEntity() { }
#pragma warning restore CS8618

    #endregion

    public SaleEntity(string saleNumber, DateTime saleDate, Guid customerId, Guid branchId)
    {
        SaleNumber = saleNumber;
        SetSaleDate(saleDate);
        CustomerId = customerId;
        BranchId = branchId;
    }

    public void AddSaleItem(SaleItemEntity itemSale)
    {
        var item = SaleItems.SingleOrDefault(i => i.ProductId == itemSale.ProductId);

        if (item is null)
        {
            SaleItems.Add(itemSale);
        }
        else
        {
            SaleItems.Remove(item);
            SaleItems.Add(item);
        }
    }

    public void CancelSale()
    {
        IsCancelled = true;
    }

    private static uint GenerateSaleNumber()
    {
        var randomBytes = new byte[4];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomBytes);
        uint trueRandom = BitConverter.ToUInt32(randomBytes, 0);

        return trueRandom;
    }

    private void SetSaleDate(DateTime saleDate)
    {
        if (saleDate.Date > DateTime.Now)
            throw new InvalidOperationException("Sale order can't be future");

        SaleDate = saleDate;
    }

    private decimal CalculateTotalToPay()
    {
        var amountToPay = 0m;

        foreach (var i in SaleItems)
            amountToPay += i.Total;

        return amountToPay;
    }
}

