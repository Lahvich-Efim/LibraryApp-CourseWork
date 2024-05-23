using System;
using System.Collections.Generic;

namespace LibraryApp.Model;

public partial class Publisher
{
    public int IdPublisher { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
