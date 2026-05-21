using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ord.EF.Models;

[Table("CUSTOMER_ADDRESS")]
public partial class CustomerAddress
{
    [Key]
    [Column("CUSTOMER_ADDRESS_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string CustomerAddressId { get; set; } = null!;

    [Column("CUSTOMER_ADDRESS_CUSTOMER_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? CustomerAddressCustomerId { get; set; }

    [Column("CUSTOMER_ADDRESS_ADDRESS_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? CustomerAddressAddressId { get; set; }

    [Column("CUSTOMER_ADDRESS_DFLT", TypeName = "NUMBER(1)")]
    public bool? CustomerAddressDflt { get; set; }

    [Column("CUSTOMER_ADDRESS_ACTV_IND", TypeName = "NUMBER(1)")]
    public bool? CustomerAddressActvInd { get; set; }

    [Column("CUSTOMER_ADDRESS_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string CustomerAddressCrtdId { get; set; } = null!;

    [Column("CUSTOMER_ADDRESS_CRTD_DT", TypeName = "DATE")]
    public DateTime CustomerAddressCrtdDt { get; set; }

    [Column("CUSTOMER_ADDRESS_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string CustomerAddressUpdtId { get; set; } = null!;

    [Column("CUSTOMER_ADDRESS_UPDT_DT", TypeName = "DATE")]
    public DateTime CustomerAddressUpdtDt { get; set; }

    [ForeignKey("CustomerAddressAddressId")]
    [InverseProperty("CustomerAddress")]
    public virtual Address? CustomerAddressAddress { get; set; }

    [ForeignKey("CustomerAddressCustomerId")]
    [InverseProperty("CustomerAddress")]
    public virtual Customer? CustomerAddressCustomer { get; set; }

    [InverseProperty("OrdersCustomerAddress")]
    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
