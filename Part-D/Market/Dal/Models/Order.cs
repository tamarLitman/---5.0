using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dal.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? OrderStateId { get; set; }

    [JsonIgnore]
    public virtual State? OrderState { get; set; }

    public virtual ICollection<Stock>? Prods { get; set; } 
}
