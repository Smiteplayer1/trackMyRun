using System;
using System.Collections.Generic;

namespace trackMyRun.DbEntities;

public partial class Shoe
{
    public int ShoeId { get; set; }

    public string ShoeName { get; set; } = null!;

    public string Width { get; set; } = null!;

    public decimal Size { get; set; }

    public string? ShoeImg { get; set; }

    public virtual ICollection<Run> Runs { get; set; } = new List<Run>();
}
