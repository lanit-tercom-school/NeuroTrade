using System.Collections.Generic;
using MetaData.Enums;

namespace MetaData.Models.TradeDataModels
{
  /// <summary>
  ///   Market model.
  /// </summary>
  public interface IMarket
  {
    /// <summary>
    ///   Id.
    /// </summary>
    int Id { get; }

    /// <summary>
    ///   Name.
    /// </summary>
    string Name { get; }

    /// <summary>
    ///   Description.
    /// </summary>
    string Description { get; }

    /// <summary>
    ///   Data source.
    /// </summary>
    DataSource DataSource { get; }

    /// <summary>
    ///   Engine.
    /// </summary>
    IEngine Engine { get; }


    /// <summary>
    ///   Boards.
    /// </summary>
    List<IBoard> GetChildBoards();
  }
}
