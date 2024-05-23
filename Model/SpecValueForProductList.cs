using System;
using System.Collections.Generic;

namespace LibraryApp.Model;

public partial class SpecValueForProductList
{
    public int SpecId { get; set; }

    public int SpecValueId { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Specification Spec { get; set; } = null!;

    public virtual SpecificationValue SpecValue { get; set; } = null!;
}
