using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ord.EF.Models;

[Table("ORDERS")]
public partial class Orders
{
    [Key]
    [Column("ORDERS_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string OrdersId { get; set; } = null!;

    [Column("ORDERS_CUSTOMER_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? OrdersCustomerId { get; set; }

    [Column("ORDERS_DATE", TypeName = "DATE")]
    public DateTime? OrdersDate { get; set; }

    [Column("ORDERS_ORDER_STATUS_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? OrdersOrderStatusId { get; set; }

    [Column("ORDERS_CUSTOMER_ADDRESS_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? OrdersCustomerAddressId { get; set; }

    [Column("ORDERS_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OrdersCrtdId { get; set; } = null!;

    [Column("ORDERS_CRTD_DT", TypeName = "DATE")]
    public DateTime OrdersCrtdDt { get; set; }

    [Column("ORDERS_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OrdersUpdtId { get; set; } = null!;

    [Column("ORDERS_UPDT_DT", TypeName = "DATE")]
    public DateTime OrdersUpdtDt { get; set; }

    [InverseProperty("OrderLineOrder")]
    public virtual ICollection<OrderLine> OrderLine { get; set; } = new List<OrderLine>();

    [ForeignKey("OrdersCustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer? OrdersCustomer { get; set; }

    [ForeignKey("OrdersCustomerAddressId")]
    [InverseProperty("Orders")]
    public virtual CustomerAddress? OrdersCustomerAddress { get; set; }

    [ForeignKey("OrdersOrderStatusId")]
    [InverseProperty("Orders")]
    public virtual OrderStatus? OrdersOrderStatus { get; set; }
}
