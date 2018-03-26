using System.Collections.Generic;

namespace TradeHistoryDataService.Database.Models
{
  internal class DbEngine
  {
    /// <summary>
    ///   Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///   Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///   Description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///   List of markets.
    /// </summary>
    public List<DbMarket> Markets { get; set; }
  }
}
