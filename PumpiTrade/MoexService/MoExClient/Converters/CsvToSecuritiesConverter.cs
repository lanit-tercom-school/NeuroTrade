using System.Collections.Generic;
using System.IO;
using MetaData.Models.TradeDataModels;
using MoexService.MoExClient.Models;

namespace MoexService.MoExClient.Converters
{
  /// <summary>
  ///   Csv to securities converter.
  /// </summary>
  internal static class CsvToSecuritiesConverter
  {
    #region private fields
    /// <summary>
    ///   Csv file header.
    /// </summary>
    private const string Title = "securities";

    /// <summary>
    ///   Name column.
    /// </summary>
    private const string NameColumn = "SECID";

    /// <summary>
    ///   Description column.
    /// </summary>
    private const string DescriptionColumn = "NAME";

    /// <summary>
    ///   Alternate description column.
    /// </summary>
    private const string DescriptionAlternativeColumn = "SHORTNAME";

    /// <summary>
    ///   Column separator.
    /// </summary>
    private const char ColumnSeparator = ';';
    #endregion

    #region public methods
    /// <summary>
    ///   Convert csv file to list of securities.
    /// </summary>
    public static List<ISecurity> Convert(
      StreamReader csvFile,
      IMoExClient moExClient,
      IBoard board)
    {
      ICsvFile csvFileReader = new CsvFile(csvFile);

      Dictionary<string, int> columnIndices = csvFileReader.ColumnIndices;
      List<ISecurity> securities = new List<ISecurity>();
      List<string> lines = csvFileReader.ReadFileData();

      lines.ForEach(
        line =>
        {
          var columns = line.Split(ColumnSeparator);

          Security security = new Security(moExClient, board);

          security.Name = columnIndices.ContainsKey(NameColumn)
            ? columns[columnIndices[NameColumn]]
            : string.Empty;

          security.Description = columnIndices.ContainsKey(DescriptionColumn)
            ? columns[columnIndices[DescriptionColumn]]
            : columnIndices.ContainsKey(DescriptionAlternativeColumn)
              ? columns[columnIndices[DescriptionAlternativeColumn]]
              : string.Empty;

          securities.Add(security);
        });

      return securities;
    }
    #endregion
  }
}
