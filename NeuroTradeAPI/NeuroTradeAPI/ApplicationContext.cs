using System;
using Microsoft.EntityFrameworkCore;

namespace NeuroTradeAPI
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Candle> Candles { get; set; }
        private static DbContextOptions<ApplicationContext> _defaultOptions;

        public ApplicationContext() : base(_defaultOptions)
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            _defaultOptions = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}