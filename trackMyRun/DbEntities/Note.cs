using System;
using System.Collections.Generic;

namespace trackMyRun.DbEntities;

public partial class Note
{
    public int NoteId { get; set; }

    public string NoteName { get; set; } = null!;

    public string? NoteText { get; set; }

    public int RunId { get; set; }

    public virtual Run Run { get; set; } = null!;
}
