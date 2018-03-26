using System;
using System.Collections.Generic;
using MetaData.Models.TradeDataModels;

namespace MoexService.MoExClient
{
  /// <summary>
  ///   Client for Moscow exchange.
  /// </summary>
  public interface IMoExClient
  {
    /// <summary>
    ///   Get list of boards for the given market.
    /// </summary>
    List<IBoard> GetBoards(IMarket market);

    /// <summary>
    ///   Get list of engines.
    /// </summary>
    List<IEngine> GetEngines();

    /// <summary>
    ///   Get list of markets for the given engine.
    /// </summary>
    List<IMarket> GetMarkets(IEngine engine);

    /// <summary>
    ///   Get list of securities for the given board.
    /// </summary>
    List<ISecurity> GetSecurities(IBoard board);

    /// <summary>
    ///   Get list of trades for the given security.
    /// </summary>
    List<ITrade> GetTrades(ISecurity security, DateTime from, DateTime till, int offset);

    /// <summary>
    ///   Get trade dates for security.
    /// </summary>
    Tuple<DateTime?, DateTime?> GetSecurityDates(ISecurity security);
  }
}
