using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeverageStore.Models;

[Table("Seat")]
[Index("Name", Name = "UQ__Seat__737584F6265CCF33", IsUnique = true)]
public partial class Seat
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("isAvailable")]
    public bool IsAvailable { get; set; }

    [InverseProperty("Seat")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public Seat(int id, string name, bool isAvailable)
    {
        Id = id;
        Name = name;
        IsAvailable = isAvailable;
    }
    public Seat()
    {
    }
}
