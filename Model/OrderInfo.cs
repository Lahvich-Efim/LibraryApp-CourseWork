using System;
using System.Collections.Generic;

namespace LibraryApp.Model;

public partial class OrderInfo
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime DateOrder { get; set; }

    public DateTime DateEnd { get; set; }

    public byte Status { get; set; }

    public bool IsActiv { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Product> ProductCodes { get; set; } = new List<Product>();
}
