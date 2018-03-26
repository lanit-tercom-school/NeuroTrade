using System;

namespace MetaData.Models.TradeDataModels
{
  /// <summary>
  ///   Trade model.
  /// </summary>
  public interface ITrade
  {
    /// <summary>
    ///   Id.
    /// </summary>
    int Id { get; }

    /// <summary>
    ///   Trade day date.
    /// </summary>
    DateTime TradeDate { get; }

    /// <summary>
    ///   Number of trades.
    /// </summary>
    double TradesCount { get; }

    /// <summary>
    ///   Opening price.
    /// </summary>
    double OpeningPrice { get; }

    /// <summary>
    ///   Closing price.
    /// </summary>
    double ClosingPrice { get; }

    /// <summary>
    ///   Highest price.
    /// </summary>
    double HighestPrice { get; }

    /// <summary>
    ///   Lowest price.
    /// </summary>
    double LowestPrice { get; }
  }
}
