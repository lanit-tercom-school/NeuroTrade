using System;
using System.Linq;
using MoexService.MoExClient;
using NLog;
using TradeDataLoaderService;
using TradeDataLoaderService.MoExClient;
using TradeHistoryDataService;

namespace Tester
{
  class Program
  {
    static void Main(string[] args)
    {
      LogManager.ThrowConfigExceptions = true;
      var logger = LogManager.GetCurrentClassLogger();

      Console.WriteLine("Enter postgresql password");
      string password = Console.ReadLine();

      ITradeHistoryDataService tradeHistoryDataService =
        new TradeHistoryDataService.TradeHistoryDataService(
          $"Host=localhost;Database=PumpiTrade;Username=postgres;Password={password}");
      IMoExClient moExClient = new MoExClient();
      ITradeDataLoaderService tradeDataLoader =
        new TradeDataLoaderService.TradeDataLoaderService(
          moExClient,
          tradeHistoryDataService);

      logger.Info("Job started");

      logger.Info("Downloading trade data for currency securities");
      var currencySecurities = tradeHistoryDataService.Securities
        .GetAll()
        .Where(s => s.Board.Market.Engine.Name == "currency")
        .ToList();
      currencySecurities.ForEach(security =>
      {
        try
        {
          tradeDataLoader.DownloadTradeData(security);
        }
        catch (Exception e)
        {
          logger.Error(
            $"Failed to download trade data for security {security.Name}. Error: {e}");
        }
      });
    }
  }
}
