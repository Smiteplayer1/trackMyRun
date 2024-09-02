



using Microsoft.EntityFrameworkCore;

namespace trackMyRun.DbEntities
{
    public class trackMyRunDbContext : DbContext
    {
        public trackMyRunDbContext(
            DbContextOptions<trackMyRunDbContext> options)
            : base(options) { }
    }
}
