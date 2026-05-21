using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ord.EF.Models;

[Table("PRODUCT_CATEGORY")]
public partial class ProductCategory
{
    [Key]
    [Column("PRODUCT_CATEGORY_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string ProductCategoryId { get; set; } = null!;

    [Column("PRODUCT_CATEGORY_PRODUCT_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? ProductCategoryProductId { get; set; }

    [Column("PRODUCT_CATEGORY_CATEGORY_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? ProductCategoryCategoryId { get; set; }

    [Column("PRODUCT_CATEGORY_EFF_DATE", TypeName = "DATE")]
    public DateTime? ProductCategoryEffDate { get; set; }

    [Column("PRODUCT_CATEGORY_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string ProductCategoryCrtdId { get; set; } = null!;

    [Column("PRODUCT_CATEGORY_CRTD_DT", TypeName = "DATE")]
    public DateTime ProductCategoryCrtdDt { get; set; }

    [Column("PRODUCT_CATEGORY_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string ProductCategoryUpdtId { get; set; } = null!;

    [Column("PRODUCT_CATEGORY_UPDT_DT", TypeName = "DATE")]
    public DateTime ProductCategoryUpdtDt { get; set; }

    [ForeignKey("ProductCategoryProductId")]
    [InverseProperty("ProductCategory")]
    public virtual Product? ProductCategoryProduct { get; set; }
}
