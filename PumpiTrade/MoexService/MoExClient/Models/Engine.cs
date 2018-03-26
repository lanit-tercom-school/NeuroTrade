using System.Collections.Generic;
using MetaData.Enums;
using MetaData.Models.TradeDataModels;

namespace MoexService.MoExClient.Models
{
  /// <summary>
  ///   Moex engine model.
  /// </summary>
  internal class Engine : IEngine
  {
    #region private fields
    /// <summary>
    ///   Moex client.
    /// </summary>
    private readonly IMoExClient moExClient;
    #endregion

    #region public properties
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
    ///   Data source.
    /// </summary>
    public DataSource DataSource { get; }
    #endregion

    #region public methods
    /// <summary>
    ///   Get child markets.
    /// </summary>
    public List<IMarket> GetChildMarkets()
    {
      return moExClient.GetMarkets(this);
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="Engine"/> class.
    /// </summary>
    public Engine(IMoExClient moExClient)
    {
      this.moExClient = moExClient;
      this.DataSource = DataSource.OnlineMoscowExchange;
    }
    #endregion
  }
}
