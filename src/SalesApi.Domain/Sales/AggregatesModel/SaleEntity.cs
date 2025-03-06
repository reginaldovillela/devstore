namespace SalesApi.Domain.Sales.AggregatesModel;

[Index(nameof(EntityId), IsUnique = true)]
[Index(nameof(SaleNumber), IsUnique = true)]
[Table("sales")]
public class SaleEntity
    : Entity, IAggregateRoot
{
    [Required]
    public string SaleNumber { get; init; }

    [Required]
    public DateTime SaleDate { get; private set; }

    [Required]
    public Guid CustomerId { get; init; }

    [Required]
    public Guid BranchId { get; init; }

    [Required]
    public bool IsCancelled { get; private set; } = false;

    [NotMapped]
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
        SaleItems = [];
    }

    public void AddSaleItem(SaleItemEntity itemSale)
    {
        var item = SaleItems.SingleOrDefault(i => i.ProductId == itemSale.ProductId);

        if (item is not null)
        {
            itemSale.IncreaseQuantity(item.Quantity);
            SaleItems.Remove(item);
        }

        SaleItems.Add(itemSale);
    }

    public void CancelSale()
    {
        IsCancelled = true;
    }

    public void SetSaleDate(DateTime saleDate)
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

