using System;
using System.Collections.Generic;

namespace trackMyRun.DbEntities;

public partial class Run
{
    public int RunId { get; set; }

    public float Distance { get; set; }

    public string Time { get; set; } = null!;

    public string? AvgPace { get; set; }

    public int? HeartRate { get; set; }

    public int ShoeId { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual Shoe Shoe { get; set; } = null!;
}
