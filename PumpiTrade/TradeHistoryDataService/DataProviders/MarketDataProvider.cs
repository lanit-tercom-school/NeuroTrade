using System.Collections.Generic;
using System.Linq;
using MetaData.Models.TradeDataModels;
using Microsoft.EntityFrameworkCore;
using TradeHistoryDataService.Database;
using TradeHistoryDataService.Database.Models;
using TradeHistoryDataService.Models;

namespace TradeHistoryDataService.DataProviders
{
  /// <summary>
  ///   Market data provider.
  /// </summary>
  public class MarketDataProvider
  {
    #region private fields
    /// <summary>
    ///   Database connection string.
    /// </summary>
    private readonly string connectionString;
    #endregion

    #region public methods
    /// <summary>
    ///   Get by id.
    /// </summary>
    public IMarket GetById(int id)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      return new Market(connectionString, dbContext.Markets.Find(id));
    }

    /// <summary>
    ///   Get all.
    /// </summary>
    public List<IMarket> GetAll()
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var markets = dbContext.Markets
        .Include(s => s.Engine)
        .ToList();

      return new List<IMarket>(
        markets.Select(s => new Market(connectionString, s)).ToList());
    }

    /// <summary>
    ///   Get by engine.
    /// </summary>
    public List<IMarket> GetByEngine(IEngine engine)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var markets = dbContext.Markets
        .Where(s => s.EngineId == engine.Id)
        .Include(s => s.Engine)
        .ToList();

      return new List<IMarket>(
        markets.Select(s => new Market(connectionString, s)).ToList());
    }

    /// <summary>
    ///   Add market.
    /// </summary>
    public IMarket Add(IMarket market)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var dbMarket = new DbMarket()
      {
        Description = market.Description,
        EngineId = market.Engine.Id,
        Name = market.Name
      };

      dbContext.Markets.Add(dbMarket);
      dbContext.SaveChanges();

      return new Market(connectionString, dbMarket);
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="MarketDataProvider"/> class.
    /// </summary>
    public MarketDataProvider(string connectionString)
    {
      this.connectionString = connectionString;
    }
    #endregion
  }
}
