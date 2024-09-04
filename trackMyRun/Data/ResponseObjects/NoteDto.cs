namespace trackMyRun.Data.ResponseObjects
{
    public class NoteDto
    {
        public int NoteId { get; set; }
        public string NoteName { get; set; } = null!;

        public string? NoteText { get; set; }

        public int RunId { get; set; }

    }
}
