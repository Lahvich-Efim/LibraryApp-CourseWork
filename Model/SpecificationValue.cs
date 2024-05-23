using System;
using System.Collections.Generic;

namespace LibraryApp.Model;

public partial class SpecificationValue
{
    public int SpecValueId { get; set; }

    public int? PvalueString { get; set; }

    public decimal? ValueInt { get; set; }

    public int SpecId { get; set; }

    public virtual Dictionary? PvalueStringNavigation { get; set; }

    public virtual ICollection<SpecValueForProductList> SpecValueForProductLists { get; set; } = new List<SpecValueForProductList>();
}
