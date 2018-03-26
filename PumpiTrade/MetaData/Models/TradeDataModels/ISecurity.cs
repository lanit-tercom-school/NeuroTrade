using System;
using System.Collections.Generic;
using MetaData.Enums;

namespace MetaData.Models.TradeDataModels
{
  /// <summary>
  ///   Security model.
  /// </summary>
  public interface ISecurity
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
    ///   Trade data exists from.
    /// </summary>
    DateTime? TradeDataExistsFrom { get; }

    /// <summary>
    ///   Trade data exists till.
    /// </summary>
    DateTime? TradeDataExistsTill { get; }

    /// <summary>
    ///   Data source.
    /// </summary>
    DataSource DataSource { get; }

    /// <summary>
    ///   Board.
    /// </summary>
    IBoard Board { get; }

    /// <summary>
    ///   Get list of trades.
    /// </summary>
    List<ITrade> GetTrades(DateTime from, DateTime till);

    /// <summary>
    ///   Get list of trades.
    /// </summary>
    List<ITrade> GetTrades(int count, int offset);

    /// <summary>
    ///   Get count of trades.
    /// </summary>
    int GetTradesCount();

    /// <summary>
    ///   Update data exists dates.
    /// </summary>
    void UpdateDataExistsDates();
  }
}
