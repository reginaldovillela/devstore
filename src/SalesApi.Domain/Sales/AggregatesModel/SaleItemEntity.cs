using System.ComponentModel.DataAnnotations;

namespace SalesApi.Domain.Sales.AggregatesModel;

[Table("sales_items")]
public class SaleItemEntity
    : Entity
{
    public Guid ProductId { get; init; }

    public ushort Quantity { get; private set; }

    public decimal UnitPrice { get; init; }

    public decimal Discount { get; private set; } = 0;

    public decimal Total => CalculateTotalToPay();

    public bool IsCancelled { get; private set; } = false;

    #region "ef requirements and relations"

    [ForeignKey("sale")]
    [Required]
    public Guid SaleId { get; private init; }

    public SaleEntity Sale { get; private set; } = null!;

#pragma warning disable CS8618
    protected SaleItemEntity() { }
#pragma warning restore CS8618

    #endregion

    public SaleItemEntity(Guid productId, ushort quantity, decimal unitPrice )
    {
        ProductId = productId;
        SetQuantity(quantity);
        UnitPrice = unitPrice;
    }

    public void IncreaseQuantity()
    {
        SetQuantity(Quantity++);
    }

    private void SetQuantity(ushort quantity)
    {
        if (quantity > 20)
            throw new InvalidOperationException("You can buy only 20 pices of a item");

        Quantity = quantity;

        CalculeDiscount();
    }

    private void CalculeDiscount()
    {
        if (Quantity >= 4)
            Discount = 10;

        if (Quantity >= 10)
            Discount = 0;
    }
    
    private decimal CalculateTotalToPay()
    {
        var discont = Discount / 100;

        return (UnitPrice * Quantity) * discont;
    }
}

