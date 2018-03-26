using System;

namespace TradeHistoryDataService.Database.Models
{
  internal class DbTrade
  {
    /// <summary>
    ///   Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///   Security id.
    /// </summary>
    public int SecurityId { get; set; }

    /// <summary>
    ///   Trade day date.
    /// </summary>
    public DateTime TradeDate { get; set; }

    /// <summary>
    ///   Number of trades.
    /// </summary>
    public double TradesCount { get; set; }

    /// <summary>
    ///   Opening price.
    /// </summary>
    public double OpeningPrice { get; set; }

    /// <summary>
    ///   Closing price.
    /// </summary>
    public double ClosingPrice { get; set; }

    /// <summary>
    ///   Highest price.
    /// </summary>
    public double HighestPrice { get; set; }

    /// <summary>
    ///   Lowest price.
    /// </summary>
    public double LowestPrice { get; set; }

    /// <summary>
    ///   Security.
    /// </summary>
    public DbSecurity Security { get; set; }
  }
}
