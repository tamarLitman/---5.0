
using System.Text.Json.Serialization;

namespace Dal;

public partial class State
{
    public int StateId { get; set; }

    public string? StateDescription { get; set; }
   
    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
