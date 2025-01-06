using Microsoft.EntityFrameworkCore;

namespace SpendSmart.Models
{
    public class SpendSmartDbContext : DbContext
    {
        public DbSet<Expense> Expenseses { get; set; }
        public SpendSmartDbContext(DbContextOptions<SpendSmartDbContext> options)
            : base(options) // explicitly calls the constructor of the DbContext base class and passes the options parameter to it.
        {
           
        }
    }
}
