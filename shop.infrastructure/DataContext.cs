using activity.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace activity.infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Activity> Activities { get; set; }
    }
}
