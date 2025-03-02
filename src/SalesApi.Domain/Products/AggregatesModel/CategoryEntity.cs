namespace SalesApi.Domain.Products.AggregatesModel;

[Table("categories")]
public class CategoryEntity
: Entity
{
    public string Title { get; init; }

    public string Description { get; init; }

    #region "ef requirements and relations"

#pragma warning disable CS8618
    protected CategoryEntity() { }
#pragma warning restore CS8618

    #endregion

}

