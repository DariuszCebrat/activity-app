using Microsoft.EntityFrameworkCore;
using shop.domain.Entities;

namespace shop.infrastructure
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Activity> Activities { get; set; }
    }
}
