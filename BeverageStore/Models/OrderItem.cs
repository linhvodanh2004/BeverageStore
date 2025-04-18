using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeverageStore.Models;

[Table("OrderItem")]
public partial class OrderItem
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int BeverageId { get; set; }

    public int Quantity { get; set; }

    [Column("isPrepared")]
    public bool IsPrepared { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal OrderItemPrice { get; set; }

    [ForeignKey("BeverageId")]
    [InverseProperty("OrderItems")]
    public virtual Beverage Beverage { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order Order { get; set; } = null!;
    public OrderItem(int id, int orderId, int beverageId, int quantity, bool isPrepared, decimal orderItemPrice)
    {
        Id = id;
        OrderId = orderId;
        BeverageId = beverageId;
        Quantity = quantity;
        IsPrepared = isPrepared;
        OrderItemPrice = orderItemPrice;
    }
    public OrderItem()
    {
    }
}
