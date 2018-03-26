using System.Collections.Generic;
using System.IO;
using MetaData.Models.TradeDataModels;
using MoexService.MoExClient.Models;

namespace MoexService.MoExClient.Converters
{
  /// <summary>
  ///   Csv file to engines converter.
  /// </summary>
  internal static class CsvToEnginesConverter
  {
    #region private fields
    /// <summary>
    ///   Csv header.
    /// </summary>
    private const string Title = "engines";

    /// <summary>
    ///   Name column.
    /// </summary>
    private const string NameColumn = "name";

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
    ///   Convert csv file to list of engines.
    /// </summary>
    public static List<IEngine> Convert(
      StreamReader csvFile,
      IMoExClient moExClient)
    {
      ICsvFile csvFileReader = new CsvFile(csvFile);

      Dictionary<string, int> columnIndices = csvFileReader.ColumnIndices;
      List<IEngine> engines = new List<IEngine>();
      List<string> lines = csvFileReader.ReadFileData();

      lines.ForEach(
        line =>
        {
          var columns = line.Split(ColumnSeparator);

          Engine engine = new Engine(moExClient);

          engine.Name = columns[columnIndices[NameColumn]];
          engine.Description = columns[columnIndices[DescriptionColumn]];

          engines.Add(engine);
        });

      return engines;
    }
    #endregion
  }
}
