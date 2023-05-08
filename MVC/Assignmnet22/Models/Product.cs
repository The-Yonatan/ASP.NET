using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Assignmnet22.Models
{
    [Table("products")]
    [Index(nameof(CategoryId), Name = "categories_products")]
    [Index(nameof(CategoryId), Name = "category_id")]
    [Index(nameof(ProductName), Name = "product_name")]
    [Index(nameof(SupplierId), Name = "supplier_id")]
    [Index(nameof(SupplierId), Name = "suppliers_products")]
    public partial class Product
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("product_name")]
        [StringLength(40)]
        [Display (Name = "Product")]
        public string ProductName { get; set; } = null!;
        [Column("supplier_id")]
        public int? SupplierId { get; set; }
        [Column("category_id")]
        public int? CategoryId { get; set; }
        [Column("quantity_per_unit")]
        [StringLength(20)]
        public string? QuantityPerUnit { get; set; }
        [Column("unit_price", TypeName = "money")]
        [Display(Name ="Price")]
        public decimal? UnitPrice { get; set; }
        [Column("units_in_stock")]
        [Display(Name = "InStock")]
        public short? UnitsInStock { get; set; }
        [Column("units_on_order")]
        public short? UnitsOnOrder { get; set; }
        [Column("reorder_level")]
        public short? ReorderLevel { get; set; }
        [Column("discontinued")]
        public bool Discontinued { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual Category? Category { get; set; }
        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("Products")]
        public virtual Supplier? Supplier { get; set; }
    }
}
