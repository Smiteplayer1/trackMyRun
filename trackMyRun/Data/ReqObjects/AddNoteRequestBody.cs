namespace trackMyRun.Data.ReqObjects
{
    public class AddNoteRequestBody
    {

        public string NoteName { get; set; } = null!;

        public string? NoteText { get; set; }

        public int RunId { get; set; }

    }
}
