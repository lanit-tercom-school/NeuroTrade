namespace MoexService.MoExClient.Constants
{
  /// <summary>
  ///   Moex service endpoints.
  /// </summary>
  internal class Endpoints
  {
    /// <summary>
    ///   List of engines.
    /// </summary>
    public const string Engines = "iss/engines.csv";

    /// <summary>
    ///   Engine.
    /// </summary>
    public const string Engine = "iss/engines/{0}.csv";

    /// <summary>
    ///   List of markets for the given engine.
    /// </summary>
    public const string Markets = "iss/engines/{0}/markets.csv";

    /// <summary>
    ///   Market of given engine.
    /// </summary>
    public const string Market = "iss/engines/{0}/markets/{1}.csv";

    /// <summary>
    ///   List of boards for the given engine and market.
    /// </summary>
    public const string Boards = "iss/engines/{0}/markets/{1}/boards.csv";

    /// <summary>
    ///   Board of the given engine and market.
    /// </summary>
    public const string Board = "iss/engines/{0}/markets/{1}/boards/{2}.csv";

    /// <summary>
    ///   List of securities for the given board.
    /// </summary>
    public const string Securities =
      "iss/engines/{0}/markets/{1}/boards/{2}/securities.csv";

    /// <summary>
    ///   Datetime period of available trade data.
    /// </summary>
    public const string SecurityDates =
      "iss/history/engines/{0}/markets/{1}/boards/{2}/securities/{3}/dates.csv";

    /// <summary>
    ///   List of trades for the given security.
    /// </summary>
    public const string SecurityTrades =
      "iss/history/engines/{0}/markets/{1}/boards/{2}/securities/{3}.csv?from={4}&till={5}&start={6}";
  }
}
