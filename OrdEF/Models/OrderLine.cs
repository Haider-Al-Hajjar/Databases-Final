using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ord.EF.Models;

[Table("ORDER_LINE")]
public partial class OrderLine
{
    [Key]
    [Column("ORDER_LINE_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string OrderLineId { get; set; } = null!;

    [Column("ORDER_LINE_ORDER_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? OrderLineOrderId { get; set; }

    [Column("ORDER_LINE_PRODUCT_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? OrderLineProductId { get; set; }

    [Column("ORDER_LINE_QTY")]
    [Precision(5)]
    public short? OrderLineQty { get; set; }

    [Column("ORDER_LINE_UNIT_PRICE", TypeName = "NUMBER(9,2)")]
    public decimal? OrderLineUnitPrice { get; set; }

    [Column("ORDER_LINE_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OrderLineCrtdId { get; set; } = null!;

    [Column("ORDER_LINE_CRTD_DT", TypeName = "DATE")]
    public DateTime OrderLineCrtdDt { get; set; }

    [Column("ORDER_LINE_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OrderLineUpdtId { get; set; } = null!;

    [Column("ORDER_LINE_UPDT_DT", TypeName = "DATE")]
    public DateTime OrderLineUpdtDt { get; set; }

    [ForeignKey("OrderLineOrderId")]
    [InverseProperty("OrderLine")]
    public virtual Orders? OrderLineOrder { get; set; }

    [ForeignKey("OrderLineProductId")]
    [InverseProperty("OrderLine")]
    public virtual Product? OrderLineProduct { get; set; }
}
