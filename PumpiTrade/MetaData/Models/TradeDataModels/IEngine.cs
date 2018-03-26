using System.Collections.Generic;
using MetaData.Enums;

namespace MetaData.Models.TradeDataModels
{
  /// <summary>
  ///   Engine model.
  /// </summary>
  public interface IEngine
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
    ///   Get child markets.
    /// </summary>
    List<IMarket> GetChildMarkets();
  }
}
