using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeverageStore.Models;

[Table("Category")]
[Index("Name", Name = "UQ__Category__737584F618AC66B0", IsUnique = true)]
public partial class Category
{
    public Category()
    {
    }

    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("Category")]
    public virtual ICollection<Beverage> Beverages { get; set; } = new List<Beverage>();

    public Category(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
