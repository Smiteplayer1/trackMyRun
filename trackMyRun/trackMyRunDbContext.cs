



using Microsoft.EntityFrameworkCore;

namespace trackMyRun
{
    public class trackMyRunDbContext : DbContext
    {
        public trackMyRunDbContext(
            DbContextOptions<trackMyRunDbContext> options) 
            : base(options) { }
    }
}
