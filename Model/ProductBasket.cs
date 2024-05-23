namespace LibraryApp.Model;

public partial class ProductBasket
{
    public int BasketId { get; set; }

    public int ProductCode { get; set; }

    public int? Quantity { get; set; }

    public virtual Basket Basket { get; set; } = null!;

    public virtual Product ProductCodeNavigation { get; set; } = null!;
}
