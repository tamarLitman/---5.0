using System;
using System.Collections.Generic;

namespace Dal;

public partial class Order
{
    public int OrderId { get; set; }

    public int? OrderStateId { get; set; }

    public virtual State? OrderState { get; set; }
}
