using Microsoft.EntityFrameworkCore;
using Points.Models;

namespace Points.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
