using System;
using System.Collections.Generic;

namespace LibraryApp.Model;

public partial class Specification
{
    public int SpecId { get; set; }

    public int PspecName { get; set; }

    public virtual Dictionary PspecNameNavigation { get; set; } = null!;

    public virtual ICollection<SpecValueForProductList> SpecValueForProductLists { get; set; } = new List<SpecValueForProductList>();

    public virtual ICollection<Product> ProductCodes { get; set; } = new List<Product>();
}
