using System.Collections.Generic;
using MetaData.Enums;
using MetaData.Models.TradeDataModels;

namespace MoexService.MoExClient.Models
{
  /// <summary>
  ///   Moex makret model.
  /// </summary>
  internal class Market : IMarket
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

    /// <summary>
    ///   Engine.
    /// </summary>
    public IEngine Engine { get; }
    #endregion

    #region public methods
    /// <summary>
    ///   Boards.
    /// </summary>
    public List<IBoard> GetChildBoards()
    {
      return moExClient.GetBoards(this);
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="Market"/> class.
    /// </summary>
    public Market(IMoExClient moExClient, IEngine engine)
    {
      this.moExClient = moExClient;
      this.Engine = engine;
      this.DataSource = DataSource.OnlineMoscowExchange;
    }
    #endregion
  }
}
