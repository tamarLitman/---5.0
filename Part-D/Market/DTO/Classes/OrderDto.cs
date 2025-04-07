using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DTO.Classes;

public partial class OrderDto
{
    public int OrderId { get; set; }

    public int? OrderStateId { get; set; }

    public string? State { get; set; }

    public List<StockDto>? Prods { get; set; }
}
