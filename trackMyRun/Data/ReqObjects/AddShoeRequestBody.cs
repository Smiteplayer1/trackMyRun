using trackMyRun.DbEntities;

namespace trackMyRun.Data.ReqObjects
{
    public class AddShoeRequestBody
    {
        public string ShoeName { get; set; } = null!;

        public string Width { get; set; } = null!;

        public decimal Size { get; set; }

        public string? ShoeImg { get; set; }
    }
}
