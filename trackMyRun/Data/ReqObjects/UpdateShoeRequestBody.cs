using trackMyRun.DbEntities;

namespace trackMyRun.Data.ReqObjects
{
    public class UpdateShoeRequestBody
    {
        public int ShoeId { get; set; }
        public string ShoeName { get; set; } = null!;

        public string Width { get; set; } = null!;

        public decimal Size { get; set; }

        public string? ShoeImg { get; set; }

    }
}
