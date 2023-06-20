using company_service.Models;
using Microsoft.EntityFrameworkCore;

namespace company_service
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Company> Сompanies { get; set; }
        public DbSet<IntershipPosition> IntershipPositions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
