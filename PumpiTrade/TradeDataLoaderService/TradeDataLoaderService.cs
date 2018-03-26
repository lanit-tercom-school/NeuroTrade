using System;
using System.Linq;
using MetaData.Models.TradeDataModels;
using MoexService.MoExClient;
using NLog;
using TradeHistoryDataService;

namespace TradeDataLoaderService
{
  /// <summary>
  ///   Trade data loader service.
  /// </summary>
  public class TradeDataLoaderService : ITradeDataLoaderService
  {
    #region private fields
    /// <summary>
    ///   Moex client.
    /// </summary>
    private readonly IMoExClient moexClient;

    /// <summary>
    ///   Trade history data service.
    /// </summary>
    private readonly ITradeHistoryDataService tradeHistoryDataService;

    /// <summary>
    ///   Logger.
    /// </summary>
    private readonly Logger logger = LogManager.GetCurrentClassLogger();
    #endregion

    #region private methods
    /// <summary>
    ///   Fetch engines.
    /// </summary>
    private void FetchEngines()
    {
      logger.Info("Fetching engines...");

      var dbEngines = tradeHistoryDataService.Engines.GetAll();
      var moexEngines = moexClient.GetEngines();

      moexEngines.ForEach(
        s =>
        {
          if (dbEngines.Any(c => c.Name == s.Name))
          {
            return;
          }

          tradeHistoryDataService.Engines.Add(s);
          logger.Info($"Engine {s.Name} successfully added.");
        });
    }

    /// <summary>
    ///   Fetch markets.
    /// </summary>
    private void FetchMarkets(IEngine engine)
    {
      logger.Info($"Fetching markets for engine {engine.Name}...");

      var dbMarkets = tradeHistoryDataService.Markets.GetByEngine(engine);
      var moexMarkets = moexClient.GetMarkets(engine);

      moexMarkets.ForEach(
        s =>
        {
          if (dbMarkets.Any(c => c.Name == s.Name))
          {
            return;
          }

          tradeHistoryDataService.Markets.Add(s);
          logger.Info($"Market {s.Name} successfully added.");
        });
    }

    /// <summary>
    ///   Fetch boards.
    /// </summary>
    private void FetchBoards(IMarket market)
    {
      logger.Info($"Fetching boards for market {market.Name}...");

      var dbBoards = tradeHistoryDataService.Boards.GetByMarket(market);
      var moexBoards = moexClient.GetBoards(market);

      moexBoards.ForEach(
        s =>
        {
          if (dbBoards.Any(c => c.Name == s.Name))
          {
            return;
          }

          tradeHistoryDataService.Boards.Add(s);
          logger.Info($"Board {s.Name} successfully added.");
        });
    }

    /// <summary>
    ///   Fetch securities.
    /// </summary>
    private void FetchSecurities(IBoard board)
    {
      logger.Info($"Fetching securities for board {board.Name}...");

      var dbSecurities = tradeHistoryDataService.Securities.GetByBoard(board);
      var moexSecurities = moexClient.GetSecurities(board);
      var notAddedSecurities =
        moexSecurities
          .Where(s => dbSecurities.All(p => p.Name != s.Name))
          ?.ToList();

      if (notAddedSecurities == null || notAddedSecurities.Count == 0)
      {
        return;
      }

      tradeHistoryDataService.Securities.AddRange(notAddedSecurities);

      notAddedSecurities.ForEach(
        s => logger.Info($"Security {s.Name} successfully added."));
    }

    /// <summary>
    ///   Fetch data.
    /// </summary>
    public void FetchData()
    {
      FetchEngines();

      var engines = tradeHistoryDataService.Engines.GetAll();

      engines.ForEach(FetchMarkets);

      var markets = tradeHistoryDataService.Markets.GetAll();

      markets.ForEach(FetchBoards);

      var boards = tradeHistoryDataService.Boards.GetAll();

      boards.ForEach(FetchSecurities);
    }
    #endregion

    /// <summary>
    ///   Download trade data for security.
    /// </summary>
    public void DownloadTradeData(ISecurity security)
    {
      var tradeDates = moexClient.GetSecurityDates(security);

      if (!tradeDates.Item1.HasValue || !tradeDates.Item2.HasValue)
      {
        logger.Info($"No trade data available for security {security.Name}");
        return;
      }

      logger.Info(
        $"Starting downloading trades for security {security.Name}, trades exists from {tradeDates.Item1.Value.ToShortDateString()} till {tradeDates.Item2.Value.ToShortDateString()}.");

      DateTime startDateTime =
        security.TradeDataExistsTill ?? tradeDates.Item1.Value;
      DateTime logStartTime =
        security.TradeDataExistsTill ?? tradeDates.Item1.Value;
      while (startDateTime < tradeDates.Item2.Value)
      {
        var trades = moexClient.GetTrades(
          security,
          startDateTime,
          tradeDates.Item2.Value,
          0);

        startDateTime = trades.Last().TradeDate.AddDays(1);

        tradeHistoryDataService.Trades.AddRange(trades, security);

        logger.Info(
          $"{100 * (startDateTime - logStartTime).TotalDays / (tradeDates.Item2.Value - logStartTime).TotalDays}% completed.");
      }

      logger.Info(
        $"Trade data for security {security.Name} successfully downloaded.");
    }

    /// <summary>
    ///   Update trade data.
    /// </summary>
    public void FetchTradeData()
    {
      var markets = tradeHistoryDataService.Markets.GetAll();

      markets.ForEach(
        market =>
        {
          logger.Info($"Fetcging trade data for market {market.Name}");
          var securities = tradeHistoryDataService.Securities
            .GetAll()
            .Where(s => s.Board.Market.Name == market.Name)
            .ToList();
          try
          {
            securities.ForEach(DownloadTradeData);
          }
          catch (Exception e)
          {
            logger.Error($"Failed to fetch data for market {market.Name}. Error: {e}");
          }
        });
    }

    public TradeDataLoaderService(
      IMoExClient moexClient,
      ITradeHistoryDataService tradeHistoryDataService)
    {
      this.moexClient = moexClient;
      this.tradeHistoryDataService = tradeHistoryDataService;
    }
  }
}
