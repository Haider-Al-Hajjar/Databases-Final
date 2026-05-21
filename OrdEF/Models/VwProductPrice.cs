using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ord.EF.Models;

[Keyless]
public partial class VwProductPrice
{
    [Column("PRODUCT_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string ProductId { get; set; } = null!;

    [Column("PRODUCT_NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ProductName { get; set; }

    [Column("PRODUCT_PRICE_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string ProductPriceId { get; set; } = null!;

    [Column("PRODUCT_PRICE_PRICE", TypeName = "NUMBER(9,2)")]
    public decimal? ProductPricePrice { get; set; }

    [Column("STARTDATE", TypeName = "DATE")]
    public DateTime? Startdate { get; set; }

    [Column("ENDDATE", TypeName = "DATE")]
    public DateTime? Enddate { get; set; }
}
