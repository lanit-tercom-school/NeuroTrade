using System.Collections.Generic;
using System.Linq;
using MetaData.Enums;
using MetaData.Models.TradeDataModels;
using Microsoft.EntityFrameworkCore;
using TradeHistoryDataService.Database;
using TradeHistoryDataService.Database.Models;

namespace TradeHistoryDataService.Models
{
  /// <summary>
  ///   Database engine model.
  /// </summary>
  internal class Engine : IEngine
  {
    #region private fields
    /// <summary>
    ///   Database connection string.
    /// </summary>
    private readonly string connectionString;
    #endregion

    #region public properties
    /// <summary>
    ///   Id.
    /// </summary>
    public int Id { get; }

    /// <summary>
    ///   Name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///   Description.
    /// </summary>
    public string Description { get; }

    /// <summary>
    ///   Data source.
    /// </summary>
    public DataSource DataSource { get; }
    #endregion

    #region public methods
    /// <summary>
    ///   Get child markets.
    /// </summary>
    public List<IMarket> GetChildMarkets()
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var dbMarkets = dbContext.Engines
        .Include(s => s.Markets)
        .First(s => s.Id == this.Id)
        .Markets;

      return new List<IMarket>(
        dbMarkets
          .Select(s => new Market(connectionString, s))
          ?.ToList());
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="Engine"/> class.
    /// </summary>
    public Engine(string connectionString, DbEngine engine)
    {
      if (engine == null)
      {
        return;
      }

      this.connectionString = connectionString;
      DataSource = DataSource.LocalDatabase;
      Id = engine.Id;
      Name = engine.Name;
      Description = engine.Description;
    }
    #endregion
  }
}
