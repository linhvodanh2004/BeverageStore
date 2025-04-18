using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeverageStore.Models;

[Table("Ingredient")]
[Index("Name", Name = "UQ__Ingredie__737584F67D1462FB", IsUnique = true)]
public partial class Ingredient
{
    public Ingredient()
    {
    }

    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [ForeignKey("IngredientId")]
    [InverseProperty("Ingredients")]
    public virtual ICollection<Beverage> Beverages { get; set; } = new List<Beverage>();

    public Ingredient(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
