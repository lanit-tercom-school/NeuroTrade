using Microsoft.EntityFrameworkCore;

namespace NeuroTradeAPI
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Candle> Candles { get; set; }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=nt_db;Username=nt_dev;Password=weakpassword");
        }
    }
}