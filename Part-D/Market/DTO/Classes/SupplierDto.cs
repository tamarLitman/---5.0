using Dal.Models;
using System;
using System.Collections.Generic;

namespace DTO.Classes;

public partial class SupplierDto
{
    public int SupplierId { get; set; }

    public string? SupllierName { get; set; }

    public string? CompanyName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Representative { get; set; }

    public virtual ICollection<StockDto> Stocks { get; set; } = new List<StockDto>();

}

