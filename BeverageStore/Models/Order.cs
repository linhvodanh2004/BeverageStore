using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeverageStore.Models;

[Table("Order")]
public partial class Order
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndedTime { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalPrice { get; set; }

    public int SeatId { get; set; }

    [StringLength(250)]
    public string Note { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("SeatId")]
    [InverseProperty("Orders")]
    public virtual Seat Seat { get; set; } = null!;
    public Order(int id, DateTime createdTime, DateTime endedTime, decimal totalPrice, int seatId, string note)
    {
        Id = id;
        CreatedTime = createdTime;
        EndedTime = endedTime;
        TotalPrice = totalPrice;
        SeatId = seatId;
        Note = note;
    }

    public Order()
    {
    }
}
