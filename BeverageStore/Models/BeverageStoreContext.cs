using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BeverageStore.Models;

public partial class BeverageStoreContext : DbContext
{
    public BeverageStoreContext()
    {
    }

    public BeverageStoreContext(DbContextOptions<BeverageStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beverage> Beverages { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("BeverageStore"));
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beverage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Beverage__3214EC07868E0516");

            entity.Property(e => e.IsAvailable).HasDefaultValue(true);

            entity.HasOne(d => d.Category).WithMany(p => p.Beverages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Beverage__Catego__5165187F");

            entity.HasMany(d => d.Ingredients).WithMany(p => p.Beverages)
                .UsingEntity<Dictionary<string, object>>(
                    "BeverageIngredient",
                    r => r.HasOne<Ingredient>().WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BeverageI__Ingre__5535A963"),
                    l => l.HasOne<Beverage>().WithMany()
                        .HasForeignKey("BeverageId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BeverageI__Bever__5441852A"),
                    j =>
                    {
                        j.HasKey("BeverageId", "IngredientId").HasName("PK__Beverage__8FA5B8ECF88CF9B5");
                        j.ToTable("BeverageIngredient");
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC0758F9A821");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0797CCE015");

            entity.Property(e => e.IsActivated).HasDefaultValue(true);

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__RoleId__3E52440B");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingredie__3214EC07F79B0E42");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC07943F0E1E");

            entity.Property(e => e.CreatedTime).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Seat).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__SeatId__46E78A0C");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderIte__3214EC079427A138");

            entity.HasOne(d => d.Beverage).WithMany(p => p.OrderItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Bever__5AEE82B9");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__59FA5E80");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07064192D1");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seat__3214EC07C6B10FEF");

            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
