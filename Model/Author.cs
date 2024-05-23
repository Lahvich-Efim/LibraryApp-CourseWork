using System;
using System.Collections.Generic;

namespace LibraryApp.Model;

public partial class Author
{
    public int AuthorId { get; set; }

    public int Palias { get; set; }

    public virtual Dictionary PaliasNavigation { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
