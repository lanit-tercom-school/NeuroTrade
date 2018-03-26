using System.Collections.Generic;

namespace TradeHistoryDataService.Database.Models
{
  internal class DbBoard
  {
    /// <summary>
    ///   Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///   Market id.
    /// </summary>
    public int MarketId { get; set; }

    /// <summary>
    ///   Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///   Description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///   Market.
    /// </summary>
    public DbMarket Market { get; set; }

    /// <summary>
    ///   Securities.
    /// </summary>
    public List<DbSecurity> Securities { get; set; }
  }
}
