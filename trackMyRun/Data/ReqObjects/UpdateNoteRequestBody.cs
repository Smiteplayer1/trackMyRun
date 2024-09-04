namespace trackMyRun.Data.ReqObjects
{
    public class UpdateNoteRequestBody
    {
        public int NoteId { get; set; }

        public string NoteName { get; set; } = null!;

        public string? NoteText { get; set; }

        public int RunId { get; set; }

    }
}
