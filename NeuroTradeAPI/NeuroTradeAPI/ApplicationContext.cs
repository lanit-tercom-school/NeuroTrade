using System;
using Microsoft.EntityFrameworkCore;

namespace NeuroTradeAPI
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Candle> Candles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}