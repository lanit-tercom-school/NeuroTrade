using System;
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
  ///   Database security model.
  /// </summary>
  internal class Security : ISecurity
  {
    #region private fields
    /// <summary>
    ///   Database connection string.
    /// </summary>
    private readonly string connectionString;
    #endregion

    #region private methods
    /// <summary>
    ///   Get parent board.
    /// </summary>
    private IBoard GetBoard(DbSecurity dbSecurity)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var dbBoard = dbContext.Boards
        .Include(s => s.Market)
        .ThenInclude(s => s.Engine)
        .First(s => s.Id == dbSecurity.BoardId);

      return
        new Board(
        connectionString,
        dbBoard);
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
    ///   Trade data exists from.
    /// </summary>
    public DateTime? TradeDataExistsFrom { get; set; }

    /// <summary>
    ///   Trade data exists till.
    /// </summary>
    public DateTime? TradeDataExistsTill { get; set; }

    /// <summary>
    ///   Data source.
    /// </summary>
    public DataSource DataSource { get; }

    /// <summary>
    ///   Board.
    /// </summary>
    public IBoard Board { get; }
    #endregion

    #region public methods
    /// <summary>
    ///   Get list of trades.
    /// </summary>
    public List<ITrade> GetTrades(DateTime from, DateTime till)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var dbTrades = dbContext.Securities
        .Include(
          s => s.Trades.Where(c => c.TradeDate >= from && c.TradeDate <= till))
        .First(s => s.Id == Id)
        ?.Trades
        .ToList();

      return new List<ITrade>(
        dbTrades.Select(s => new Trade(connectionString, s)));
    }

    /// <summary>
    ///   Get list of trades.
    /// </summary>
    public List<ITrade> GetTrades(int count, int offset)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var dbTrades = dbContext.Securities
        .Include(
          s => s.Trades
            .OrderBy(p => p.TradeDate)
            .Where((c, index) => index >= offset && index < offset + count))
        .First(p => p.Id == Id)
        .Trades
        .ToList();

      return new List<ITrade>(
        dbTrades.Select(s => new Trade(connectionString, s)));
    }

    /// <summary>
    ///   Get count of trades.
    /// </summary>
    public int GetTradesCount()
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      return dbContext.Trades
        .Count(s => s.SecurityId == this.Id);
    }

    /// <summary>
    ///   Update data exists date.
    /// </summary>
    public void UpdateDataExistsDates()
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var dbSecurity = dbContext.Securities.Find(Id);

      TradeDataExistsFrom = dbSecurity.TradeDataExistsFrom;
      TradeDataExistsTill = dbSecurity.TradeDataExistsTill;
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="Security"/> class.
    /// </summary>
    public Security(string connectionString, DbSecurity dbSecurity)
    {
      this.connectionString = connectionString;
      Id = dbSecurity.Id;
      Name = dbSecurity.Name;
      Description = dbSecurity.Description;
      TradeDataExistsFrom = dbSecurity.TradeDataExistsFrom;
      TradeDataExistsTill = dbSecurity.TradeDataExistsTill;
      DataSource = DataSource.LocalDatabase;
      Board = dbSecurity.Board == null
        ? GetBoard(dbSecurity)
        : new Board(connectionString, dbSecurity.Board);
    }
    #endregion
  }
}
