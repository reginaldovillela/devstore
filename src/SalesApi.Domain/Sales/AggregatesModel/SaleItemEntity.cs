namespace SalesApi.Domain.Sales.AggregatesModel;

[Index(nameof(EntityId), IsUnique = true)]
[Index(nameof(ProductId))]
[Table("sales_items")]
public class SaleItemEntity
    : Entity
{
    [Required]
    public Guid ProductId { get; init; }

    [Required]
    public ushort Quantity { get; private set; } = 0;

    [Required]
    public decimal UnitPrice { get; private set; } = 0;

    [Required]
    public int PercentDiscount { get; private set; } = 0;

    [Required]
    public bool IsCancelled { get; private set; } = false;

    [NotMapped]
    public decimal TotalDiscount => CalculateTotalDiscount();

    [NotMapped]
    public decimal Total => CalculateTotalToPay();

    #region "ef requirements and relations"

    [ForeignKey("sale")]
    [Required]
    public Guid SaleId { get; private init; }

    public SaleEntity Sale { get; private set; } = null!;

#pragma warning disable CS8618
    protected SaleItemEntity() { }
#pragma warning restore CS8618

    #endregion

    public SaleItemEntity(Guid productId, ushort quantity, decimal unitPrice)
    {
        ProductId = productId;
        SetQuantity(quantity);
        SetUnitPrice(unitPrice);
    }

    public void IncreaseQuantity(ushort quantityToAdd = 1)
    {
        var newQuantity = (ushort)(Quantity + quantityToAdd);
        SetQuantity(newQuantity);
    }

    public void SetQuantity(ushort quantity)
    {
        if (quantity > 20)
            throw new InvalidOperationException("You can buy only 20 pices of a item");

        Quantity = quantity;

        CalculePercentDiscount();
    }

    public void SetUnitPrice(decimal unitPrice)
    {
        if (unitPrice <= 0)
            throw new InvalidOperationException("Unit Price cannot be equal 0 or less than 0");

        UnitPrice = unitPrice;
    }

    private void CalculePercentDiscount()
    {
        if (Quantity >= 4)
            PercentDiscount = 10;

        if (Quantity >= 10)
            PercentDiscount = 20;
    }

    private decimal CalculateTotalDiscount()
    {
        var discount = (decimal)PercentDiscount / 100;
        var totalWithoutDiscount = UnitPrice * Quantity;
        return discount * totalWithoutDiscount;
    }

    private decimal CalculateTotalToPay()
    {
        var totalWithoutDiscount = UnitPrice * Quantity;
        return totalWithoutDiscount - TotalDiscount;
    }
}

