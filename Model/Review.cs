using System;
using System.Collections.Generic;

namespace LibraryApp.Model;

public partial class Review
{
    public int ReviewId { get; set; }

    public int ProductCode { get; set; }

    public int UserId { get; set; }

    public byte Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime ReviewDate { get; set; }

    public virtual Product ProductCodeNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
