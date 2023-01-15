using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using FastFood.Models;

namespace FastFood.Areas.Identity.Data;

public class FastFoodUser : IdentityUser
{
    [Required(ErrorMessage = "First name is required")]
    [Display(Name = "First Name")]
    [StringLength(30 ,ErrorMessage = "can't pass 30 letters")]
    public string FirstName { get; set; } = null!;
    [Required(ErrorMessage = "Last name is required")]
    [Display(Name = "Last Name")]
    [StringLength(30, ErrorMessage = "can't pass 30 letters")]
    public string LastName { get; set; } = null!;
    public string? Region { get; set; } = null!;
    public string? City { get; set; } = null!;
    public string? Address { get; set; } = null!;
    [DataType(DataType.CreditCard)]
    public string? CreditCard { get; set; } = null!;
    public virtual ICollection<Order>? Orders { get; set; }

}

