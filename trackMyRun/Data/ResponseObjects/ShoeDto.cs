using trackMyRun.DbEntities;

namespace trackMyRun.Data.ResponseObjects
{
    public class ShoeDto
    {
        public int Id { get; set; }
        public string ShoeName { get; set; } = null!;

        public string Width { get; set; } = null!;

        public decimal Size { get; set; }

        public string? ShoeImg { get; set; }

    }
}
