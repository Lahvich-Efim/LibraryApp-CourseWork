using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Model;

public partial class User
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "ErrorLogin")]
    [StringLength(45, MinimumLength = 3, ErrorMessage = "ErrorLogin")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "ErrorEmail")]
    [EmailAddress(ErrorMessage = "ErrorEmail")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "ErrorPassword")]
    [StringLength(45, MinimumLength = 3, ErrorMessage = "ErrorPassword")]
    public string PasswordHash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string? FatherName { get; set; }
    
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }


    public string? Phone { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public string? Pavatar { get; set; }

    public bool IsAdmin { get; set; }

    public short Theme { get; set; }

    public short Mode { get; set; }

    public short ModeNotify { get; set; }

    public virtual Basket? Basket { get; set; }

    public virtual Library? Library { get; set; }

    public virtual ICollection<Notify> Notifies { get; set; } = new List<Notify>();

    public virtual ICollection<OrderInfo> OrderInfos { get; set; } = new List<OrderInfo>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
