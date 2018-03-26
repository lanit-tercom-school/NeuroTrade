using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoexService.MoExClient.Models;

namespace MoexService.MoExClient.Converters
{
  /// <summary>
  ///   Csv to available trade dates converter.
  /// </summary>
  internal static class CsvToDateTimePeriodsConverter
  {
    #region private fields
    /// <summary>
    ///   Data exists from column.
    /// </summary>
    private const string FromColumnName = "from";

    /// <summary>
    ///   Data exists till column.
    /// </summary>
    private const string TillColumnName = "till";

    /// <summary>
    ///   Csv file header.
    /// </summary>
    private const string Title = "dates";

    /// <summary>
    ///   Column separator.
    /// </summary>
    private const char ColumnSeparator = ';';
    #endregion

    #region public methods
    /// <summary>
    ///   Convert csv file to trade date periods.
    /// </summary>
    public static Tuple<DateTime?, DateTime?> Convert(StreamReader csvFile)
    {
      ICsvFile csvFileReader = new CsvFile(csvFile);

      Dictionary<string, int> columnIndices = csvFileReader.ColumnIndices;

      string line = csvFileReader
        .ReadFileData()
        .FirstOrDefault();

      if (string.IsNullOrEmpty(line))
      {
        return new Tuple<DateTime?, DateTime?>(null, null);
      }
      var columns = line.Split(ColumnSeparator);

      if (!DateTime.TryParse(
        columns[columnIndices[FromColumnName]],
        out var from) || !DateTime.TryParse(columns[columnIndices[TillColumnName]], out var till))
      {
        return new Tuple<DateTime?, DateTime?>(null, null);
      }

      return new Tuple<DateTime?, DateTime?>(from, till);
    }
    #endregion
  }
}
