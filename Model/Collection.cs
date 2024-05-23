using System;
using System.Collections.Generic;

namespace LibraryApp.Model;

public partial class Collection
{
    public int CollectionId { get; set; }

    public string Name { get; set; } = null!;

    public int LibraryId { get; set; }

    public bool? IsDefault { get; set; }

    public virtual Library Library { get; set; } = null!;

    public virtual ICollection<Product> ProductCodes { get; set; } = new List<Product>();
}
