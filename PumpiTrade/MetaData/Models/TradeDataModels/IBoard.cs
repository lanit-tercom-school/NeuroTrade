using System.Collections.Generic;
using MetaData.Enums;

namespace MetaData.Models.TradeDataModels
{
  /// <summary>
  ///   Board model.
  /// </summary>
  public interface IBoard
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
    ///   DataSource
    /// </summary>
    DataSource DataSource { get; }

    /// <summary>
    ///   Market
    /// </summary>
    IMarket Market { get; }

    /// <summary>
    ///   Securities.
    /// </summary>
    List<ISecurity> GetChildSecurities();
  }
}
