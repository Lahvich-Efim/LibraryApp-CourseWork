namespace LibraryApp.Model;

public partial class OrderItem
{
    public int OrderId { get; set; }

    public int ProductCode { get; set; }

    public int? Quantity { get; set; }

    public virtual OrderInfo Order { get; set; } = null!;

    public virtual Product ProductCodeNavigation { get; set; } = null!;
}
