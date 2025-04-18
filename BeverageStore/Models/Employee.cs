using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeverageStore.Models;

[Table("Employee")]
[Index("Username", Name = "UQ__Employee__536C85E40B04F636", IsUnique = true)]
[Index("Phone", Name = "UQ__Employee__5C7E359E2960A4F8", IsUnique = true)]
[Index("Email", Name = "UQ__Employee__A9D10534436EFE93", IsUnique = true)]
public partial class Employee
{
    public Employee()
    {
    }

    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    [StringLength(100)]
    public string Fullname { get; set; } = null!;

    public DateOnly Dob { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string Phone { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    public bool Gender { get; set; }

    [Column("isActivated")]
    public bool IsActivated { get; set; }

    public int RoleId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("Employees")]
    public virtual Role Role { get; set; } = null!;

    public Employee(int id, string username, string password, string fullname, DateOnly dob, string phone, string email, bool gender, bool isActivated, int roleId)
    {
        Id = id;
        Username = username;
        Password = password;
        Fullname = fullname;
        Dob = dob;
        Phone = phone;
        Email = email;
        Gender = gender;
        IsActivated = isActivated;
        RoleId = roleId;
    }
}
