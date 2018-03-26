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
  ///   Trade data provider.
  /// </summary>
  public class TradeDataProvider
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
    public ITrade GetById(int id)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      return new Trade(connectionString, dbContext.Trades.Find(id));
    }

    /// <summary>
    ///   Get all.
    /// </summary>
    public List<ITrade> GetAll()
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var trades = dbContext.Trades
        .Include(s => s.Security)
        .ThenInclude(s => s.Board)
        .ThenInclude(s => s.Market)
        .ThenInclude(s => s.Engine)
        .ToList();

      return new List<ITrade>(
        trades.Select(s => new Trade(connectionString, s)).ToList());
    }

    /// <summary>
    ///   Get trades for security.
    /// </summary>
    public List<ITrade> GetTrades(ISecurity security)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var trades =
        dbContext.Securities
          .Include(s => s.Trades)
          .ThenInclude(s => s.Security)
          .First(s => s.Id == security.Id)
          .Trades;

      return new List<ITrade>(
        trades
          .Select(s => new Trade(connectionString, s))
          .ToList());
    }

    /// <summary>
    ///   Get portion of trades for security.
    /// </summary>
    public List<ITrade> GetTrades(ISecurity security, int count, int offset)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var trades =
        dbContext.Trades
          .Where(s => s.SecurityId == security.Id)
          .Include(s => s.Security)
          .ThenInclude(s => s.Board)
          .ThenInclude(s => s.Market)
          .ThenInclude(s => s.Engine)
          .OrderBy(s => s.TradeDate)
          .Skip(offset)
          .Take(count)
          .ToList();

      if (trades.Count < offset + count)
      {
        return new List<ITrade>();
      }

      trades.RemoveRange(0, offset);

      return new List<ITrade>(
        trades
          .Take(count)
          .Select(s => new Trade(connectionString, s))
          .ToList());
    }

    /// <summary>
    ///   Add trade.
    /// </summary>
    public ITrade Add(ITrade trade, ISecurity security)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var dbTrade = new DbTrade()
      {
        ClosingPrice = trade.ClosingPrice,
        HighestPrice = trade.HighestPrice,
        LowestPrice = trade.LowestPrice,
        OpeningPrice = trade.OpeningPrice,
        SecurityId = security.Id,
        TradeDate = trade.TradeDate,
        TradesCount = trade.TradesCount
      };

      dbContext.Trades.Add(dbTrade);
      dbContext.SaveChanges();

      return new Trade(connectionString, dbTrade);
    }

    /// <summary>
    ///   Add trades.
    /// </summary>
    public void AddRange(List<ITrade> trades, ISecurity security)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      foreach (var trade in trades)
      {
        var dbTrade = new DbTrade()
        {
          ClosingPrice = trade.ClosingPrice,
          HighestPrice = trade.HighestPrice,
          LowestPrice = trade.LowestPrice,
          OpeningPrice = trade.OpeningPrice,
          SecurityId = security.Id,
          TradeDate = trade.TradeDate,
          TradesCount = trade.TradesCount
        };

        dbContext.Trades.Add(dbTrade);
      }

      var dbSecurity = dbContext.Securities.Find(security.Id);

      if (!dbSecurity.TradeDataExistsFrom.HasValue ||
          dbSecurity.TradeDataExistsFrom > trades.Min(s => s.TradeDate))
      {
        dbSecurity.TradeDataExistsFrom = trades.Min(s => s.TradeDate);
      }

      if (!dbSecurity.TradeDataExistsTill.HasValue ||
          dbSecurity.TradeDataExistsTill < trades.Max(s => s.TradeDate))
      {
        dbSecurity.TradeDataExistsTill = trades.Max(s => s.TradeDate);
      }

      dbContext.SaveChanges();
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="TradeDataProvider"/> class.
    /// </summary>
    public TradeDataProvider(string connectionString)
    {
      this.connectionString = connectionString;
    }
    #endregion
  }
}
