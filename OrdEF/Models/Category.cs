using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ord.EF.Models;

[Table("CATEGORY")]
public partial class Category
{
    [Key]
    [Column("CATEGORY_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string CategoryId { get; set; } = null!;

    [Column("CATEGORY_NAME")]
    [StringLength(200)]
    [Unicode(false)]
    public string? CategoryName { get; set; }

    [Column("CATEGORY_PRNT_CATEGORY_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? CategoryPrntCategoryId { get; set; }

    [Column("CATEGORY_CATEGORY_TYPE_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? CategoryCategoryTypeId { get; set; }

    [Column("CATEGORY_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string CategoryCrtdId { get; set; } = null!;

    [Column("CATEGORY_CRTD_DT", TypeName = "DATE")]
    public DateTime CategoryCrtdDt { get; set; }

    [Column("CATEGORY_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string CategoryUpdtId { get; set; } = null!;

    [Column("CATEGORY_UPDT_DT", TypeName = "DATE")]
    public DateTime CategoryUpdtDt { get; set; }

    [ForeignKey("CategoryCategoryTypeId")]
    [InverseProperty("Category")]
    public virtual CategoryType? CategoryCategoryType { get; set; }

    [ForeignKey("CategoryPrntCategoryId")]
    [InverseProperty("InverseCategoryPrntCategory")]
    public virtual Category? CategoryPrntCategory { get; set; }

    [InverseProperty("CategoryPrntCategory")]
    public virtual ICollection<Category> InverseCategoryPrntCategory { get; set; } = new List<Category>();
}
