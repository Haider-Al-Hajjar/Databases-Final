using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ord.EF.Models;

[Table("PRODUCT")]
public partial class Product
{
    [Key]
    [Column("PRODUCT_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string ProductId { get; set; } = null!;

    [Column("PRODUCT_NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ProductName { get; set; }

    [Column("PRODUCT_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string ProductCrtdId { get; set; } = null!;

    [Column("PRODUCT_CRTD_DT", TypeName = "DATE")]
    public DateTime ProductCrtdDt { get; set; }

    [Column("PRODUCT_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string ProductUpdtId { get; set; } = null!;

    [Column("PRODUCT_UPDT_DT", TypeName = "DATE")]
    public DateTime ProductUpdtDt { get; set; }

    [InverseProperty("OrderLineProduct")]
    public virtual ICollection<OrderLine> OrderLine { get; set; } = new List<OrderLine>();

    [InverseProperty("ProductCategoryProduct")]
    public virtual ICollection<ProductCategory> ProductCategory { get; set; } = new List<ProductCategory>();

    [InverseProperty("ProductPriceProduct")]
    public virtual ICollection<ProductPrice> ProductPrice { get; set; } = new List<ProductPrice>();
}
