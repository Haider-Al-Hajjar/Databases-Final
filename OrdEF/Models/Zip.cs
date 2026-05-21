using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ord.EF.Models;

[Table("ZIP")]
public partial class Zip
{
    [Key]
    [Column("ZIPCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Zipcode { get; set; } = null!;

    [Column("ZIPCODE_TYPE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ZipcodeType { get; set; }

    [Column("CITY")]
    [StringLength(30)]
    [Unicode(false)]
    public string? City { get; set; }

    [Column("STATE")]
    [StringLength(2)]
    [Unicode(false)]
    public string? State { get; set; }

    [Column("ZIP_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string ZipCrtdId { get; set; } = null!;

    [Column("ZIP_CRTD_DT", TypeName = "DATE")]
    public DateTime ZipCrtdDt { get; set; }

    [Column("ZIP_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string ZipUpdtId { get; set; } = null!;

    [Column("ZIP_UPDT_DT", TypeName = "DATE")]
    public DateTime ZipUpdtDt { get; set; }

    [InverseProperty("AddressZipcodeNavigation")]
    public virtual ICollection<Address> Address { get; set; } = new List<Address>();
}
