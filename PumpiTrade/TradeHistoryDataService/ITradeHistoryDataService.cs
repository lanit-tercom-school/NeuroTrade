using TradeHistoryDataService.DataProviders;

namespace TradeHistoryDataService
{
  /// <summary>
  ///   Interface of Trade history data service.
  /// </summary>
  public interface ITradeHistoryDataService
  {
    /// <summary>
    ///   Boards.
    /// </summary>
    BoardDataProvider Boards { get; }

    /// <summary>
    ///   Engines.
    /// </summary>
    EngineDataProvider Engines { get; }

    /// <summary>
    ///   Markets.
    /// </summary>
    MarketDataProvider Markets { get; }

    /// <summary>
    ///   Securities.
    /// </summary>
    SecurityDataProvider Securities { get; }

    /// <summary>
    ///   Trades.
    /// </summary>
    TradeDataProvider Trades { get; }
  }
}
