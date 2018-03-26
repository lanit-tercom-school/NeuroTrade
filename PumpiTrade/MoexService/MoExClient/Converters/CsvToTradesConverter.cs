using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using MetaData.Models.TradeDataModels;
using MoexService.MoExClient.Models;

namespace MoexService.MoExClient.Converters
{
  /// <summary>
  ///   Csv to trades converter.
  /// </summary>
  internal static class CsvToTradesConverter
  {
    #region private fields
    /// <summary>
    ///   Title.
    /// </summary>
    private const string Title = "history";

    /// <summary>
    ///   Column separator.
    /// </summary>
    private const char ColumnSeparator = ';';

    /// <summary>
    ///   Trade date column.
    /// </summary>
    private const string TradeDateColumn = "TRADEDATE";

    /// <summary>
    ///   Number of trades column.
    /// </summary>
    private const string NumberOfTradesColumn = "NUMTRADES";

    /// <summary>
    ///   Lowest price column.
    /// </summary>
    private const string LowestPriceColumn = "LOW";

    /// <summary>
    ///   Highest price column.
    /// </summary>
    private const string HighestPriceColumn = "HIGH";

    /// <summary>
    ///   Opening price column.
    /// </summary>
    private const string OpeningPriceColumn = "OPEN";

    /// <summary>
    ///   Closing price column.
    /// </summary>
    private const string ClosingPriceColumn = "CLOSE";

    /// <summary>
    ///   Closing price alternate column.
    /// </summary>
    private const string ClosingPriceAlternateColumn = "LAST";
    #endregion

    #region private methods
    private static double ConvertToDouble(string val)
    {
      if (string.IsNullOrEmpty(val))
      {
        throw new NullReferenceException("Cannot convert null value");
      }

      return double.Parse(val, CultureInfo.InvariantCulture);
    }

    private static bool ValidateColumns(string[] actualColumns)
    {
      if (!actualColumns.Contains(ClosingPriceColumn) &&
          !actualColumns.Contains(ClosingPriceAlternateColumn))
      {
        return false;
      }

      if (!actualColumns.Contains(OpeningPriceColumn) ||
          !actualColumns.Contains(LowestPriceColumn) ||
          !actualColumns.Contains(HighestPriceColumn) ||
          !actualColumns.Contains(TradeDateColumn))
      {
        return false;
      }

      return true;
    }
    #endregion

    #region public methods
    /// <summary>
    ///   Convert csv file to list of trades.
    /// </summary>
    public static List<ITrade> Convert(StreamReader csvFile, ISecurity security)
    {
      ICsvFile csvFileReader = new CsvFile(csvFile);

      Dictionary<string, int> columnIndices = csvFileReader.ColumnIndices;

      if (!ValidateColumns(columnIndices.Keys.ToArray()))
      {
        throw new Exception(
          $"Security {security.Name} has wrong columns metadata.");
      }

      List<ITrade> trades = new List<ITrade>();
      List<string> lines = csvFileReader.ReadFileData();

      lines.ForEach(
        line =>
        {
          var columns = line.Split(ColumnSeparator);

          Trade trade = new Trade(security);

          if (columnIndices.ContainsKey(ClosingPriceColumn))
          {
            trade.ClosingPrice =
              ConvertToDouble(columns[columnIndices[ClosingPriceColumn]]);
          }
          else if (columnIndices.ContainsKey(ClosingPriceAlternateColumn))
          {
            trade.ClosingPrice = ConvertToDouble(
              columns[columnIndices[ClosingPriceAlternateColumn]]);
          }

          if (columnIndices.ContainsKey(OpeningPriceColumn))
          {
            trade.OpeningPrice =
              ConvertToDouble(columns[columnIndices[OpeningPriceColumn]]);
          }

          if (columnIndices.ContainsKey(HighestPriceColumn))
          {
            trade.HighestPrice =
              ConvertToDouble(columns[columnIndices[HighestPriceColumn]]);
          }

          if (columnIndices.ContainsKey(LowestPriceColumn))
          {
            trade.LowestPrice =
              ConvertToDouble(columns[columnIndices[LowestPriceColumn]]);
          }

          if (columnIndices.ContainsKey(NumberOfTradesColumn))
          {
            trade.TradesCount = ConvertToDouble(
              columns[columnIndices[NumberOfTradesColumn]]);
          }

          if (DateTime.TryParse(
            columns[columnIndices[TradeDateColumn]],
            out var tradeDate))
          {
            trade.TradeDate = tradeDate;
          }

          trades.Add(trade);
        });

      return trades;
    }
  }
  #endregion
}
