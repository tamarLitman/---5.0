using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string? SupllierName { get; set; }

    public string? CompanyName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Representative { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
