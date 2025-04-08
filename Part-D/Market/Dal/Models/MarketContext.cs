using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dal.Models;

public partial class MarketContext : DbContext
{
    public MarketContext()
    {
    }

    public MarketContext(DbContextOptions<MarketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-S04DEMU\\SQLEXPRESS;Database=Market;TrustServerCertificate=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__46466601CC4BD40C");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("order_Id");
            entity.Property(e => e.OrderStateId).HasColumnName("order_state_Id");

            entity.HasOne(d => d.OrderState).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStateId)
                .HasConstraintName("FK__orders__order_st__3D5E1FD2");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("SupplierPerOrder");

            entity.HasMany(d => d.Prods).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderStock",
                    r => r.HasOne<Stock>().WithMany()
                        .HasForeignKey("ProdId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__order_Sto__Prod___4AB81AF0"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__order_Sto__order__49C3F6B7"),
                    j =>
                    {
                        j.HasKey("OrderId", "ProdId").HasName("PK__order_St__6A13DBF0255AC3F5");
                        j.ToTable("order_Stock");
                        j.IndexerProperty<int>("OrderId").HasColumnName("order_Id");
                        j.IndexerProperty<int>("ProdId").HasColumnName("Prod_Id");
                    });
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__states__81DD17CF3444EA8F");

            entity.ToTable("states");

            entity.Property(e => e.StateId)
                .ValueGeneratedNever()
                .HasColumnName("state_Id");
            entity.Property(e => e.StateDescription)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("state_Description");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.ProdId).HasName("PK__Stock__C55BDF1352D6ADEB");

            entity.ToTable("Stock");

            entity.Property(e => e.ProdId).HasColumnName("Prod_Id");
            entity.Property(e => e.MinAmount).HasColumnName("Min_Amount");
            entity.Property(e => e.Price).HasColumnType("numeric(5, 2)");
            entity.Property(e => e.ProdName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Prod_Name");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_Id");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__Stock__supplier___38996AB5");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__supplier__6ED840E050AEC55B");

            entity.ToTable("supplier");

            entity.Property(e => e.SupplierId).HasColumnName("supplier_Id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("company_Name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone_Number");
            entity.Property(e => e.Representative)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("representative");
            entity.Property(e => e.SupllierName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("supllier_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
