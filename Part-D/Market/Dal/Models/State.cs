using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class State
{
    public int StateId { get; set; }

    public string? StateDescription { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
