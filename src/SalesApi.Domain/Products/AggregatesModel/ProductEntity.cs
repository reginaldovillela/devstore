using System.ComponentModel.DataAnnotations;

namespace SalesApi.Domain.Products.AggregatesModel;

[Table("products")]
public class ProductEntity
    : Entity, IAggregateRoot
{
    public string Title { get; init; }

    public string Description { get; init; }

    public decimal Price { get; init; }

    public string Image { get; init; }

    #region "ef requirements and relations"

    [ForeignKey("Category")]
    [Required]
    public Guid CategoryId { get; private init; }

    public CategoryEntity Category { get; private set; } = null!;

#pragma warning disable CS8618
    protected ProductEntity() { }
#pragma warning restore CS8618

    #endregion

}
