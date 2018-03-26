using System.Collections.Generic;
using MetaData.Enums;
using MetaData.Models.TradeDataModels;

namespace MoexService.MoExClient.Models
{
  /// <summary>
  ///   Moex board model.
  /// </summary>
  internal class Board : IBoard
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
    ///   DataSource.
    /// </summary>
    public DataSource DataSource { get; }

    /// <summary>
    ///   Market.
    /// </summary>
    public IMarket Market { get; }
    #endregion

    #region public methods
    /// <summary>
    ///   Securities.
    /// </summary>
    public List<ISecurity> GetChildSecurities()
    {
      return moExClient.GetSecurities(this);
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="Board"/> class.
    /// </summary>
    public Board(IMoExClient moExClient, IMarket market)
    {
      this.moExClient = moExClient;
      this.Market = market;
      this.DataSource = DataSource.OnlineMoscowExchange;
    }
    #endregion
  }
}
