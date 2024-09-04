namespace trackMyRun.Data.ReqObjects
{
    public class AddRunRequestBody
    {
        public string RunName { get; set; } = null!;

        public float Distance { get; set; }

        public string Time { get; set; } = null!;

        public string? AvgPace { get; set; }

        public int? HeartRate { get; set; }

        public int ShoeId { get; set; }

    }
}
