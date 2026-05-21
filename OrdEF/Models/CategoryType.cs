using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ord.EF.Models;

[Table("CATEGORY_TYPE")]
public partial class CategoryType
{
    [Key]
    [Column("CATEGORY_TYPE_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string CategoryTypeId { get; set; } = null!;

    [Column("CATEGORY_TYPE_DESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? CategoryTypeDesc { get; set; }

    [Column("CATEGORY_TYPE_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string CategoryTypeCrtdId { get; set; } = null!;

    [Column("CATEGORY_TYPE_CRTD_DT", TypeName = "DATE")]
    public DateTime CategoryTypeCrtdDt { get; set; }

    [Column("CATEGORY_TYPE_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string CategoryTypeUpdtId { get; set; } = null!;

    [Column("CATEGORY_TYPE_UPDT_DT", TypeName = "DATE")]
    public DateTime CategoryTypeUpdtDt { get; set; }

    [InverseProperty("CategoryCategoryType")]
    public virtual ICollection<Category> Category { get; set; } = new List<Category>();
}
