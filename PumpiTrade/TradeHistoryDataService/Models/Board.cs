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
  ///   Database board model.
  /// </summary>
  internal class Board : IBoard
  {
    #region private fields
    /// <summary>
    ///   Database connection string.
    /// </summary>
    private readonly string connectionString;
    #endregion

    #region private methods
    /// <summary>
    ///   Get market.
    /// </summary>
    private IMarket GetMarket(DbBoard dbBoard)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var dbMarket = dbContext.Markets
        .Include(s => s.Engine)
        .First(s => s.Id == dbBoard.MarketId);

      return new Market(
        connectionString,
        dbMarket);
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
    ///   DataSource
    /// </summary>
    public DataSource DataSource { get; }

    /// <summary>
    ///   Market
    /// </summary>
    public IMarket Market { get; }
    #endregion

    #region public methods
    /// <summary>
    ///   Get child securities.
    /// </summary>
    public List<ISecurity> GetChildSecurities()
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var dbSecurities = dbContext.Boards
        .Include(s => s.Securities)
        .First(s => s.Id == this.Id)
        .Securities;

      return new List<ISecurity>(
        dbSecurities
          .Select(s => new Security(connectionString, s))
          .ToList());
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="Board"/> class.
    /// </summary>
    public Board(string connectionString, DbBoard dbBoard)
    {
      this.connectionString = connectionString;
      Id = dbBoard.Id;
      Name = dbBoard.Name;
      Description = dbBoard.Description;
      DataSource = DataSource.LocalDatabase;
      Market = dbBoard.Market == null
        ? GetMarket(dbBoard)
        : new Market(connectionString, dbBoard.Market);
    }
    #endregion
  }
}
