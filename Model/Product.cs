using System;
using System.Collections.Generic;

namespace LibraryApp.Model;

public partial class Product
{
    public int ProductCode { get; set; }

    public int? Year { get; set; }

    public string? DescriptionEn { get; set; }

    public string? DescriptionRus { get; set; }

    public int? NumberPages { get; set; }

    public int CatalogId { get; set; }

    public string Pimage { get; set; } = null!;

    public bool IsActive { get; set; }

    public int Quantity { get; set; }

    public DateTime DateAdded { get; set; }

    public int Pname { get; set; }

    public int AuthorId { get; set; }

    public int TypeBook { get; set; }

    public int Publisher { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Category Catalog { get; set; } = null!;

    public virtual Publisher PublisherNavigation { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<SpecValueForProductList> SpecValueForProductLists { get; set; } = new List<SpecValueForProductList>();

    public virtual TypeBook TypeBookNavigation { get; set; } = null!;

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual ICollection<OrderInfo> Orders { get; set; } = new List<OrderInfo>();

    public virtual ICollection<Specification> Specs { get; set; } = new List<Specification>();
}
