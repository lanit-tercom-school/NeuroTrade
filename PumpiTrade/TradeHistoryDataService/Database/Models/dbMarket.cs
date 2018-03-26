using System.Collections.Generic;

namespace TradeHistoryDataService.Database.Models
{
  internal class DbMarket
  {
    /// <summary>
    ///   Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///   Engine id.
    /// </summary>
    public int EngineId { get; set; }

    /// <summary>
    ///   Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///   Description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///   Engine.
    /// </summary>
    public DbEngine Engine { get; set; }

    /// <summary>
    ///   Boards.
    /// </summary>
    public List<DbBoard> Boards { get; set; }
  }
}
