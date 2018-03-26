using System.Collections.Generic;

namespace MoexService.MoExClient.Models
{
  /// <summary>
  ///   Moex csv file.
  /// </summary>
  internal interface ICsvFile
  {
    /// <summary>
    ///   Header of the csv file.
    /// </summary>
    string Header { get; }

    /// <summary>
    ///   List of column indices. 
    /// </summary>
    Dictionary<string, int> ColumnIndices { get; }

    /// <summary>
    ///   Read data from file.
    /// </summary>
    List<string> ReadFileData();
  }
}
