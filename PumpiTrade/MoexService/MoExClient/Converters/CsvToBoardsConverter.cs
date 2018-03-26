using System.Collections.Generic;
using System.IO;
using MetaData.Models.TradeDataModels;
using MoexService.MoExClient.Models;
using NLog;

namespace MoexService.MoExClient.Helpers
{
  /// <summary>
  ///   Csv to boards converter.
  /// </summary>
  internal static class CsvToBoardsConverter
  {
    #region private fields
    /// <summary>
    ///   Title column.
    /// </summary>
    private const string Title = "boards";

    /// <summary>
    ///   Name column.
    /// </summary>
    private const string NameColumn = "boardid";

    /// <summary>
    ///   Description column.
    /// </summary>
    private const string DescriptionColumn = "title";

    /// <summary>
    ///   Column separator.
    /// </summary>
    private const char ColumnSeparator = ';';

    /// <summary>
    ///   Logger.
    /// </summary>
    private static Logger logger = LogManager.GetCurrentClassLogger();
    #endregion

    #region public methods
    /// <summary>
    ///   Convert csv file to list of boards.
    /// </summary>
    public static List<IBoard> Convert(
      StreamReader csvFile,
      IMoExClient moExClient,
      IMarket market)
    {
      ICsvFile csvFileReader = new CsvFile(csvFile);

      if (csvFileReader.Header != Title)
      {
        logger.Error(
          $"Unexpected header. Expected: {Title}, but was: {csvFileReader.Header}");
        return null;
      }

      Dictionary<string, int> columnIndices = csvFileReader.ColumnIndices;
      List<IBoard> boards = new List<IBoard>();
      List<string> lines = csvFileReader.ReadFileData();
      lines.ForEach(
        s =>
        {
          var columns = s.Split(ColumnSeparator);

          Board board = new Board(moExClient, market);
          board.Description = columns[columnIndices[DescriptionColumn]];
          board.Name = columns[columnIndices[NameColumn]];

          boards.Add(board);
        });

      return boards;
    }
    #endregion
  }
}

