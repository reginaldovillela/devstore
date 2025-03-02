using System.ComponentModel.DataAnnotations;

namespace SalesApi.Domain.Sales.AggregatesModel;

[Table("items_sales")]
public class ItemSaleEntity
    : Entity
{
    public Guid ProductId { get; init; }

    public ushort Quantity { get; init; }

    public decimal UnitPrice { get; init; }

    public decimal Discount { get; init; }

    public bool IsCancelled { get; init; }

    #region "ef requirements and relations"

    [ForeignKey("Sale")]
    [Required]
    public Guid SaleId { get; private init; }

    public SaleEntity Sale { get; private set; } = null!;

#pragma warning disable CS8618
    protected ItemSaleEntity() { }
#pragma warning restore CS8618

    #endregion

}

