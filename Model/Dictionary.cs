using System;
using System.Collections.Generic;

namespace LibraryApp.Model;

public partial class Dictionary
{
    public int WordId { get; set; }

    public string WordEn { get; set; } = null!;

    public string WordRus { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<SpecificationValue> SpecificationValues { get; set; } = new List<SpecificationValue>();

    public virtual ICollection<Specification> Specifications { get; set; } = new List<Specification>();

    public virtual ICollection<TypeBook> TypeBooks { get; set; } = new List<TypeBook>();
}
