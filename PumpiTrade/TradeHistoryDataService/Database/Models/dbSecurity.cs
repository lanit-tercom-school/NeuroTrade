using System;
using System.Collections.Generic;

namespace TradeHistoryDataService.Database.Models
{
  internal class DbSecurity
  {
    /// <summary>
    ///   Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///   Board id.
    /// </summary>
    public int BoardId { get; set; }

    /// <summary>
    ///   Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///   Description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///   Board.
    /// </summary>
    public DbBoard Board { get; set; }

    /// <summary>
    ///   Trade data exists from.
    /// </summary>
    public DateTime? TradeDataExistsFrom { get; set; }

    /// <summary>
    ///   Trade data exists till.
    /// </summary>
    public DateTime? TradeDataExistsTill { get; set; }

    /// <summary>
    ///   Trades.
    /// </summary>
    public List<DbTrade> Trades { get; set; }
  }
}
