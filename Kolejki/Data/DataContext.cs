using Kolejki.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolejki.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
