using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeverageStore.Models;

[Table("Beverage")]
[Index("Name", Name = "UQ__Beverage__737584F67C2805AF", IsUnique = true)]
public partial class Beverage
{
    public Beverage()
    {
    }

    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Column("isAvailable")]
    public bool IsAvailable { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Beverages")]
    public virtual Category Category { get; set; } = null!;

    [InverseProperty("Beverage")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("BeverageId")]
    [InverseProperty("Beverages")]
    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public Beverage(int id, string name, decimal price, bool isAvailable, int categoryId, Category category)
    {
        Id = id;
        Name = name;
        Price = price;
        IsAvailable = isAvailable;
        CategoryId = categoryId;
        Category = category;
    }
}
