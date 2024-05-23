using System;
using System.Collections.Generic;

namespace LibraryApp.Model;

public partial class Category
{
    public int CategoryId { get; set; }

    public int Pname { get; set; }

    public string? Pimage { get; set; }

    public virtual Dictionary PnameNavigation { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
