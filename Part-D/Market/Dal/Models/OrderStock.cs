using System;
using System.Collections.Generic;

namespace Dal;

public partial class OrderStock
{
    public int? OrderId { get; set; }

    public int? ProdId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Stock? Prod { get; set; }
}
