
using DesignamolnlosningarLab1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesignamolnlosningarLab1.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }

}
