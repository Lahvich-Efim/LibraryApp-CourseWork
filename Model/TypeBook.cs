using System;
using System.Collections.Generic;

namespace LibraryApp.Model;

public partial class TypeBook
{
    public int Pname { get; set; }

    public int IdType { get; set; }

    public virtual Dictionary PnameNavigation { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
