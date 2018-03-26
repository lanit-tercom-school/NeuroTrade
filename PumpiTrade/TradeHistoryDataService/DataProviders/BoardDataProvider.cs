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
  ///   Board data provider.
  /// </summary>
  public class BoardDataProvider
  {
    #region private fields
    /// <summary>
    ///   Database connection string.
    /// </summary>
    private readonly string connectionString;
    #endregion

    #region public methods
    /// <summary>
    ///   Get by Id.
    /// </summary>
    public IBoard GetById(int id)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      return new Board(connectionString, dbContext.Boards.Find(id));
    }

    /// <summary>
    ///   Get all.
    /// </summary>
    public List<IBoard> GetAll()
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var boards = dbContext.Boards
        .Include(s => s.Market)
        .ThenInclude(s => s.Engine)
        .ToList();

      return new List<IBoard>(
        boards.Select(s => new Board(connectionString, s)).ToList());
    }

    /// <summary>
    ///   Get by Market.
    /// </summary>
    public List<IBoard> GetByMarket(IMarket market)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var boards = dbContext.Boards
        .Where(s => s.MarketId == market.Id)
        .Include(s => s.Market)
        .ThenInclude(s => s.Engine)
        .ToList();

      return new List<IBoard>(
        boards.Select(s => new Board(connectionString, s)).ToList());
    }

    /// <summary>
    ///   Add board.
    /// </summary>
    public IBoard Add(IBoard board)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var dbBoard = new DbBoard()
      {
        Description = board.Description,
        MarketId = board.Market.Id,
        Name = board.Name
      };

      dbContext.Boards.Add(dbBoard);
      dbContext.SaveChanges();

      return new Board(connectionString, dbBoard);
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="BoardDataProvider"/> class.
    /// </summary>
    public BoardDataProvider(string connectionString)
    {
      this.connectionString = connectionString;
    }
    #endregion
  }
}
