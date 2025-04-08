using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? OrderStateId { get; set; }

    public int? SupplierId { get; set; }

    public virtual State? OrderState { get; set; }

    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<Stock> Prods { get; set; } = new List<Stock>();
}
