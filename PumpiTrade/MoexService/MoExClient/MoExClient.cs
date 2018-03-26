using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using MetaData.Models.TradeDataModels;
using MoexService.MoExClient;
using MoexService.MoExClient.Constants;
using MoexService.MoExClient.Converters;
using MoexService.MoExClient.Helpers;

namespace TradeDataLoaderService.MoExClient
{
  /// <summary>
  ///   Client for Moscow exchange.
  /// </summary>
  public class MoExClient : IMoExClient
  {
    #region private fields
    /// <summary>
    ///   Http client.
    /// </summary>
    private readonly HttpClient httpClient;

    /// <summary>
    ///   Moex csv files encdoing.
    /// </summary>
    private readonly Encoding cyrillicEncoding =
      CodePagesEncodingProvider.Instance.GetEncoding(1251);
    #endregion

    #region private methods
    /// <summary>
    ///   Convert datetime to moex format.
    /// </summary>
    private string ConvertDateTIme(DateTime dateTime)
    {
      return $"{dateTime.Year}-{dateTime.Month}-{dateTime.Day}";
    }
    #endregion

    #region public methods
    /// <summary>
    ///   Get list of boards for the given market.
    /// </summary>
    public List<IBoard> GetBoards(IMarket market)
    {
      var response =
        httpClient.GetAsync(
            string.Format(Endpoints.Boards, market.Engine.Name, market.Name))
          .Result;

      var responseStream = response.Content.ReadAsStreamAsync().Result;
      var boards = CsvToBoardsConverter.Convert(
        new StreamReader(responseStream, cyrillicEncoding),
        this,
        market);
      return boards;
    }

    /// <summary>
    ///   Get list of engines.
    /// </summary>
    public List<IEngine> GetEngines()
    {
      var response =
        httpClient.GetAsync(Endpoints.Engines)
          .Result;

      var responseStream = response.Content.ReadAsStreamAsync().Result;

      return CsvToEnginesConverter.Convert(
        new StreamReader(responseStream, cyrillicEncoding),
        this);
    }

    /// <summary>
    ///   Get list of markets for the given engine.
    /// </summary>
    public List<IMarket> GetMarkets(IEngine engine)
    {
      var response =
        httpClient.GetAsync(string.Format(Endpoints.Markets, engine.Name))
          .Result;

      var responseStream = response.Content.ReadAsStreamAsync().Result;
      var markets = CsvToMarketsConverter.Convert(
        new StreamReader(responseStream, cyrillicEncoding),
        this,
        engine);
      return markets;
    }

    /// <summary>
    ///   Get list of securities for the given board.
    /// </summary>
    public List<ISecurity> GetSecurities(IBoard board)
    {
      var response =
        httpClient.GetAsync(
            string.Format(
              Endpoints.Securities,
              board.Market.Engine.Name,
              board.Market.Name,
              board.Name))
          .Result;

      var responseStream = response.Content.ReadAsStreamAsync().Result;
      var securities = CsvToSecuritiesConverter.Convert(
        new StreamReader(responseStream, cyrillicEncoding),
        this,
        board);
      return securities;
    }

    /// <summary>
    ///   Get list of trades for the given security.
    /// </summary>
    public List<ITrade> GetTrades(ISecurity security, DateTime from, DateTime till, int offset)
    {
      var response =
        httpClient.GetAsync(
            string.Format(
              Endpoints.SecurityTrades,
              security.Board.Market.Engine.Name,
              security.Board.Market.Name,
              security.Board.Name,
              security.Name,
              ConvertDateTIme(from),
              ConvertDateTIme(till),
              offset))
          .Result;

      var responseStream = response.Content.ReadAsStreamAsync().Result;
      var trades = CsvToTradesConverter.Convert(
        new StreamReader(responseStream, cyrillicEncoding),
        security);
      return trades;
    }

    /// <summary>
    ///   Get trade dates for security.
    /// </summary>
    public Tuple<DateTime?, DateTime?> GetSecurityDates(ISecurity security)
    {
      var response =
        httpClient.GetAsync(
            string.Format(
              Endpoints.SecurityDates,
              security.Board.Market.Engine.Name,
              security.Board.Market.Name,
              security.Board.Name,
              security.Name))
          .Result;

      var responseStream = response.Content.ReadAsStreamAsync().Result;


      return CsvToDateTimePeriodsConverter.Convert(
        new StreamReader(responseStream, cyrillicEncoding));
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="MoExClient"/> class.
    /// </summary>
    public MoExClient()
    {
      httpClient = new HttpClient();
      httpClient.BaseAddress = new Uri("http://iss.moex.com");
    }
    #endregion
  }
}
