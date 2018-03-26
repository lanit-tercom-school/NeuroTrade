using System.Collections.Generic;
using System.IO;
using MetaData.Models.TradeDataModels;
using MoexService.MoExClient.Models;

namespace MoexService.MoExClient.Converters
{
  /// <summary>
  ///   Csv to markets converter.
  /// </summary>
  internal static class CsvToMarketsConverter
  {
    #region private fields
    /// <summary>
    ///   Csv file header.
    /// </summary>
    private const string Title = "markets";

    /// <summary>
    ///   Name column.
    /// </summary>
    private const string NameColumn = "NAME";

    /// <summary>
    ///   Description column.
    /// </summary>
    private const string DescriptionColumn = "title";

    /// <summary>
    ///   Column separator.
    /// </summary>
    private const char ColumnSeparator = ';';
    #endregion

    #region public methods
    /// <summary>
    ///   Convert csv file to list of markets.
    /// </summary>
    public static List<IMarket> Convert(
      StreamReader csvFile,
      IMoExClient moExClient,
      IEngine engine)
    {
      ICsvFile csvFileReader = new CsvFile(csvFile);

      Dictionary<string, int> columnIndices = csvFileReader.ColumnIndices;
      List<IMarket> markets = new List<IMarket>();
      List<string> lines = csvFileReader.ReadFileData();

      lines.ForEach(
        line =>
        {
          var columns = line.Split(ColumnSeparator);

          Market market = new Market(moExClient, engine);

          market.Name = columns[columnIndices[NameColumn]];
          market.Description = columns[columnIndices[DescriptionColumn]];

          markets.Add(market);
        });

      return markets;
    }
    #endregion
  }
}
