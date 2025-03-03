namespace SalesApi.Domain.Products.AggregatesModel;

[Index(nameof(EntityId), IsUnique = true)]
[Index(nameof(Title), IsUnique = true)]
[Table("products")]
public class ProductEntity
    : Entity, IAggregateRoot
{
    [Required]
    public string Title { get; private set; } = string.Empty;

    [Required]
    public string Description { get; private set; } = string.Empty;

    [Required]
    public decimal Price { get; private set; }

    [Required]
    public string Category { get; private set; } = string.Empty;

    [Required]
    public string Image { get; private set; } = string.Empty;

    #region "ef requirements and relations"

    //[ForeignKey("Category")]
    //[Required]
    //public Guid CategoryId { get; private init; }

    //public CategoryEntity Category { get; private set; } = null!;

#pragma warning disable CS8618
    protected ProductEntity() { }
#pragma warning restore CS8618

    #endregion

    public ProductEntity(string title,
                         string description,
                         decimal price,
                         string category,
                         string image)
    {
        SetTitle(title);
        SetDescription(description);
        SetPrice(price);
        SetCategory(category);
        SetImage(image);
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new InvalidOperationException("Title cannot be null");

        if (title.Length < 10)
            throw new InvalidOperationException("Title must have 10 or more characters");

        Title = title;
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrEmpty(description))
            throw new InvalidOperationException("Description cannot be null");

        if (description.Length < 15)
            throw new InvalidOperationException("Description must have 15 or more characters");

        Description = description;
    }

    private void SetPrice(decimal price)
    {
        if (price <= 0)
            throw new InvalidOperationException("Price must be more than 0");

        Price = price;
    }

    private void SetCategory(string category)
    {
        Category = category;
    }

    private void SetImage(string image)
    {
        Image = image;
    }
}
