using TradeHistoryDataService.DataProviders;

namespace TradeHistoryDataService
{
  /// <summary>
  ///   Trade history data service.
  /// </summary>
  public class TradeHistoryDataService : ITradeHistoryDataService
  {
    #region public properties
    /// <summary>
    ///   Boards.
    /// </summary>
    public BoardDataProvider Boards { get; }

    /// <summary>
    ///   Engines.
    /// </summary>
    public EngineDataProvider Engines { get; }

    /// <summary>
    ///   Markets.
    /// </summary>
    public MarketDataProvider Markets { get; }

    /// <summary>
    ///   Securities.
    /// </summary>
    public SecurityDataProvider Securities { get; }

    /// <summary>
    ///   Trades.
    /// </summary>
    public TradeDataProvider Trades { get; }
    #endregion

    #region public methods
    /// <summary>
    ///   Initializes a new instance of <see cref="TradeHistoryDataService"/> class.
    /// </summary>
    public TradeHistoryDataService(string connectionString)
    {
      Boards = new BoardDataProvider(connectionString);
      Engines = new EngineDataProvider(connectionString);
      Markets = new MarketDataProvider(connectionString);
      Securities = new SecurityDataProvider(connectionString);
      Trades = new TradeDataProvider(connectionString);
    }
    #endregion
  }
}
