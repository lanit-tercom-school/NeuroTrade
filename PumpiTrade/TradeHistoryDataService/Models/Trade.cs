using System;
using MetaData.Enums;
using MetaData.Models.TradeDataModels;
using TradeHistoryDataService.Database.Models;

namespace TradeHistoryDataService.Models
{
  internal class Trade : ITrade
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
    ///   Trade day date.
    /// </summary>
    public DateTime TradeDate { get; }

    /// <summary>
    ///   Number of trades.
    /// </summary>
    public double TradesCount { get; }

    /// <summary>
    ///   Opening price.
    /// </summary>
    public double OpeningPrice { get; }

    /// <summary>
    ///   Closing price.
    /// </summary>
    public double ClosingPrice { get; }

    /// <summary>
    ///   Highest price.
    /// </summary>
    public double HighestPrice { get; }

    /// <summary>
    ///   Lowest price.
    /// </summary>
    public double LowestPrice { get; }

    /// <summary>
    ///   Data source.
    /// </summary>
    public DataSource DataSource { get; }

    /// <summary>
    ///   Security.
    /// </summary>
    public ISecurity Security { get; }
    #endregion

    #region public methods
    /// <summary>
    ///   Initializes a new instance of <see cref="Trade"/> class.
    /// </summary>
    public Trade(string connectionString, DbTrade dbTrade)
    {
      this.connectionString = connectionString;
      Id = dbTrade.Id;
      TradeDate = dbTrade.TradeDate;
      TradesCount = dbTrade.TradesCount;
      OpeningPrice = dbTrade.OpeningPrice;
      ClosingPrice = dbTrade.ClosingPrice;
      HighestPrice = dbTrade.HighestPrice;
      LowestPrice = dbTrade.LowestPrice;
      DataSource = DataSource.LocalDatabase;
    }
    #endregion
  }
}
