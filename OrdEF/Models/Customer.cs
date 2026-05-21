using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ord.EF.Models;

[Table("CUSTOMER")]
public partial class Customer
{
    [Key]
    [Column("CUSTOMER_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string CustomerId { get; set; } = null!;

    [Column("CUSTOMER_FIRST_NAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string? CustomerFirstName { get; set; }

    [Column("CUSTOMER_LAST_NAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string? CustomerLastName { get; set; }

    [Column("CUSTOMER_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string CustomerCrtdId { get; set; } = null!;

    [Column("CUSTOMER_CRTD_DT", TypeName = "DATE")]
    public DateTime CustomerCrtdDt { get; set; }

    [Column("CUSTOMER_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string CustomerUpdtId { get; set; } = null!;

    [Column("CUSTOMER_UPDT_DT", TypeName = "DATE")]
    public DateTime CustomerUpdtDt { get; set; }

    [InverseProperty("CustomerAddressCustomer")]
    public virtual ICollection<CustomerAddress> CustomerAddress { get; set; } = new List<CustomerAddress>();

    [InverseProperty("OrdersCustomer")]
    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
