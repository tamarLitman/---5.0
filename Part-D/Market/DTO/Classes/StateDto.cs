using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DTO.Classes;

public partial class StateDto
{
    public int StateId { get; set; }

    public string? StateDescription { get; set; }

}
