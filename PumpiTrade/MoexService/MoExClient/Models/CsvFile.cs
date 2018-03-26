using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using NLog.Targets;

namespace MoexService.MoExClient.Models
{
  /// <summary>
  ///   Moex csv file.
  /// </summary>
  internal class CsvFile : ICsvFile
  {
    #region private fields
    /// <summary>
    ///   Column separator.
    /// </summary>
    private const char ColumnSeparator = ';';

    /// <summary>
    ///   Position for stream to read data from.
    /// </summary>
    private readonly long dataStartPosition;

    /// <summary>
    ///   Csv file.
    /// </summary>
    private readonly StreamReader csvFile;
    #endregion

    #region private methods
    /// <summary>
    ///   Convert string from windows 1251 to UTF-8.
    /// </summary>
    #endregion

    #region public properties
    /// <summary>
    ///   Header of the csv file.
    /// </summary>
    public string Header { get; }

    /// <summary>
    ///   List of column indices. 
    /// </summary>
    public Dictionary<string, int> ColumnIndices { get; }
    #endregion

    #region public methods
    /// <summary>
    ///   Read data from file.
    /// </summary>
    public List<string> ReadFileData()
    {
      this.csvFile.BaseStream.Position = dataStartPosition;
      List<string> data = new List<string>();
      string line;
      while (!string.IsNullOrWhiteSpace(line = csvFile.ReadLine()))
      {
        data.Add(line);
      }

      return data;
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="CsvFile"/> class.
    /// </summary>
    /// <param name="csvFile"></param>
    public CsvFile(StreamReader csvFile)
    {
      this.csvFile = csvFile;
      this.csvFile.BaseStream.Position = 0;
      string line;
      while (string.IsNullOrWhiteSpace(line = this.csvFile.ReadLine()))
      {
        continue;
      }

      Header = line;

      while (string.IsNullOrWhiteSpace(line = this.csvFile.ReadLine()))
      {
        continue;
      }

      ColumnIndices = line
        .Split(ColumnSeparator)
        .Select((s, index) => new { s, index })
        .ToDictionary(p => p.s, p => p.index);

      dataStartPosition = this.csvFile.BaseStream.Position;
    }
    #endregion
  }
}
