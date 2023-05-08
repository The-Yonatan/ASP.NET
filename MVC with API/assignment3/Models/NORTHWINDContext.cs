using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace assignment3.Models
{
    public partial class NORTHWINDContext : DbContext
    {
        public NORTHWINDContext()
        {
        }

        public NORTHWINDContext(DbContextOptions<NORTHWINDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=NORTHWIND");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.HasIndex(e => e.CategoryName, "category_name");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(15)
                    .HasColumnName("category_name");

                entity.Property(e => e.Description)
                    .HasMaxLength(75)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.CategoryId, "categories_products");

                entity.HasIndex(e => e.CategoryId, "category_id");

                entity.HasIndex(e => e.ProductName, "product_name");

                entity.HasIndex(e => e.SupplierId, "supplier_id");

                entity.HasIndex(e => e.SupplierId, "suppliers_products");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Discontinued).HasColumnName("discontinued");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(40)
                    .HasColumnName("product_name");

                entity.Property(e => e.QuantityPerUnit)
                    .HasMaxLength(20)
                    .HasColumnName("quantity_per_unit");

                entity.Property(e => e.ReorderLevel)
                    .HasColumnName("reorder_level")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("unit_price")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsInStock)
                    .HasColumnName("units_in_stock")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsOnOrder)
                    .HasColumnName("units_on_order")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_products_categories");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
