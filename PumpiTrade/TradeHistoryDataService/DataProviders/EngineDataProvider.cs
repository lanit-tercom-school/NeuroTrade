using System.Collections.Generic;
using System.Linq;
using MetaData.Models.TradeDataModels;
using TradeHistoryDataService.Database;
using TradeHistoryDataService.Database.Models;
using TradeHistoryDataService.Models;

namespace TradeHistoryDataService.DataProviders
{
  /// <summary>
  /// Engine data provider.
  /// </summary>
  public class EngineDataProvider
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
    public IEngine GetById(int id)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      return new Engine(connectionString, dbContext.Engines.Find(id));
    }

    /// <summary>
    ///   Get all.
    /// </summary>
    public List<IEngine> GetAll()
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      return new List<IEngine>(
        dbContext.Engines.ToList()
          .Select(s => new Engine(connectionString, s)));
    }

    /// <summary>
    ///   Add engine.
    /// </summary>
    public IEngine Add(IEngine engine)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var dbEngine = new DbEngine
      {
        Description = engine.Description,
        Name = engine.Name
      };

      dbContext.Engines.Add(dbEngine);
      dbContext.SaveChanges();

      return new Engine(connectionString, dbEngine);
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="EngineDataProvider"/> class.
    /// </summary>
    public EngineDataProvider(string connectionString)
    {
      this.connectionString = connectionString;
    }
    #endregion
  }
}
