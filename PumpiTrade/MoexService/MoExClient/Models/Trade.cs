using System;
using MetaData.Enums;
using MetaData.Models.TradeDataModels;

namespace MoexService.MoExClient.Models
{
  /// <summary>
  ///   Moex trade model.
  /// </summary>
  internal class Trade : ITrade
  {
    #region public properties
    /// <summary>
    ///   Id.
    /// </summary>
    public int Id { get; set; }

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
    ///   Data source.
    /// </summary>
    public DataSource DataSource { get; }
    #endregion

    #region public methods
    /// <summary>
    ///   Initializes a new instance of <see cref="Trade"/> class.
    /// </summary>
    /// <param name="security"></param>
    public Trade(ISecurity security)
    {
      this.DataSource = DataSource.OnlineMoscowExchange;
    }
    #endregion
  }
}
