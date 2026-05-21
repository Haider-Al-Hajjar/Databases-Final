using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ord.EF.Models;

[Table("ORDER_STATUS")]
public partial class OrderStatus
{
    [Key]
    [Column("ORDER_STATUS_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string OrderStatusId { get; set; } = null!;

    [Column("ORDER_STATUS_DESC")]
    [StringLength(30)]
    [Unicode(false)]
    public string? OrderStatusDesc { get; set; }

    [Column("ORDER_STATUS_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OrderStatusCrtdId { get; set; } = null!;

    [Column("ORDER_STATUS_CRTD_DT", TypeName = "DATE")]
    public DateTime OrderStatusCrtdDt { get; set; }

    [Column("ORDER_STATUS_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OrderStatusUpdtId { get; set; } = null!;

    [Column("ORDER_STATUS_UPDT_DT", TypeName = "DATE")]
    public DateTime OrderStatusUpdtDt { get; set; }

    [InverseProperty("OrdersOrderStatus")]
    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
