using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Stock
{
    public int ProdId { get; set; }

    public string? ProdName { get; set; }

    public decimal? Price { get; set; }

    public int? MinAmount { get; set; }

    public int? SupplierId { get; set; }

    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
