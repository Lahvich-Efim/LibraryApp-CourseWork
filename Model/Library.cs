using System;
using System.Collections.Generic;

namespace LibraryApp.Model;

public partial class Library
{
    public int LibraryId { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual User User { get; set; } = null!;
}
