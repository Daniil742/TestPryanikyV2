using Microsoft.EntityFrameworkCore;
using TestPryanikyV2.Data.Entities;

namespace TestPryanikyV2.Data.Context
{
    public class PryanikyDbContext : DbContext
    {
        public PryanikyDbContext(DbContextOptions<PryanikyDbContext> options)
            : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
