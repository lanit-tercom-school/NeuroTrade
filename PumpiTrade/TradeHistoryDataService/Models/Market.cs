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
  ///   Database market model.
  /// </summary>
  internal class Market : IMarket
  {
    #region private fields
    /// <summary>
    ///   Database connection string.
    /// </summary>
    private readonly string connectionString;
    #endregion

    #region private methods
    /// <summary>
    ///   Get parent engine.
    /// </summary>
    private IEngine GetEngine(DbMarket market)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      return new Engine(
        connectionString,
        dbContext.Engines.Find(market.EngineId));
    }
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
    ///   Engine.
    /// </summary>
    public IEngine Engine { get; }

    /// <summary>
    ///   Data source.
    /// </summary>
    public DataSource DataSource { get; }
    #endregion

    #region public methods
    /// <summary>
    ///   Get child boards.
    /// </summary>
    public List<IBoard> GetChildBoards()
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var dbBoards = dbContext.Markets
        .Include(s => s.Boards)
        .First(s => s.Id == this.Id)
        .Boards;

      return new List<IBoard>(
        dbBoards
          .Select(s => new Board(connectionString, s))
          ?.ToList());
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="Market"/> class.
    /// </summary>
    public Market(string connectionString, DbMarket dbMarket)
    {
      this.connectionString = connectionString;
      Id = dbMarket.Id;
      Name = dbMarket.Name;
      Description = dbMarket.Description;
      DataSource = DataSource.LocalDatabase;
      Engine = dbMarket.Engine == null
        ? GetEngine(dbMarket)
        : new Engine(connectionString, dbMarket.Engine);
    }
    #endregion
  }
}
