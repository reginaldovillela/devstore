using System.Security.Cryptography;

namespace SalesApi.Domain.Sales.AggregatesModel;

[Table("sales")]
public class SaleEntity
    : Entity, IAggregateRoot
{
    public uint SaleNumber { get; init; }

    public DateTime SaleDate { get; init; }

    public Guid CustomerId { get; init; }

    public Guid BranchId { get; init; }

    public bool Cancelled { get; init; } = false;

    #region "ef requirements and relations"

    [InverseProperty("ItemsSale")]
    public ICollection<ItemSaleEntity> Items { get; private set; } = null!;

#pragma warning disable CS8618
    protected SaleEntity() { }
#pragma warning restore CS8618

    #endregion

    public SaleEntity(DateTime saleDate, Guid customerId, Guid branchId)
    {
        SaleNumber = GenerateSaleNumber();
        SaleDate = saleDate;
        CustomerId = customerId;
        BranchId = branchId;
    }

    private static uint GenerateSaleNumber()
    {
        var randomBytes = new byte[4];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomBytes);
        uint trueRandom = BitConverter.ToUInt32(randomBytes, 0);

        return trueRandom;


        //return (uint)new Random().Next(1, 999999);
    }

    public void AddItemSale(ItemSaleEntity itemSale)
    {

    }

    public void RemoveItemSale(ItemSaleEntity itemSale)
    {

    }
}

