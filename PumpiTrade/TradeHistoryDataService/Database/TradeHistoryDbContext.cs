using Microsoft.EntityFrameworkCore;
using TradeHistoryDataService.Database.Configurations;
using TradeHistoryDataService.Database.Models;

namespace TradeHistoryDataService.Database
{
  /// <summary>
  ///   Database context.
  /// </summary>
  internal sealed class TradeHistoryDbContext : DbContext
  {
    #region private fields
    /// <summary>
    ///   Database connection string.
    /// </summary>
    private readonly string connectionString;

    /// <summary>
    ///   Indicates if database was updated.
    /// </summary>
    private static bool IsDatabaseUpdated = false;
    #endregion

    #region protected methods
    /// <summary>
    ///   Database configuration.
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql(connectionString);
    }

    /// <summary>
    ///   On model creating.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new EngineConfiguration());
      modelBuilder.ApplyConfiguration(new MarketConfiguration());
      modelBuilder.ApplyConfiguration(new BoardConfiguration());
      modelBuilder.ApplyConfiguration(new SecurityConfiguration());
      modelBuilder.ApplyConfiguration(new TradeConfiguration());
    }
    #endregion

    #region public properties
    /// <summary>
    ///   Boards table.
    /// </summary>
    public DbSet<DbBoard> Boards { get; set; }

    /// <summary>
    ///   Engines table.
    /// </summary>
    public DbSet<DbEngine> Engines { get; set; }

    /// <summary>
    ///   Markets table.
    /// </summary>
    public DbSet<DbMarket> Markets { get; set; }

    /// <summary>
    ///   Securities table.
    /// </summary>
    public DbSet<DbSecurity> Securities { get; set; }

    /// <summary>
    ///   Trades table.
    /// </summary>
    public DbSet<DbTrade> Trades { get; set; }
    #endregion

    #region public methods
    /// <summary>
    ///   Initializes a new instance of <see cref="TradeHistoryDbContext"/> class.
    /// </summary>
    /// <param name="connectionString"></param>
    public TradeHistoryDbContext(string connectionString)
      : base()
    {
      this.connectionString = connectionString;

      if (!IsDatabaseUpdated)
      {
        IsDatabaseUpdated = false;
        Database.EnsureCreated();
      }
    }
    #endregion
  }
}
