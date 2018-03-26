using System;
using System.Collections.Generic;
using MetaData.Enums;
using MetaData.Models.TradeDataModels;

namespace MoexService.MoExClient.Models
{
  /// <summary>
  ///   Moex security model.
  /// </summary>
  internal class Security : ISecurity
  {
    #region priavte fields
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
    ///   Trade data exists from.
    /// </summary>
    public DateTime? TradeDataExistsFrom { get; set; }

    /// <summary>
    ///   Trade data exists till.
    /// </summary>
    public DateTime? TradeDataExistsTill { get; set; }

    /// <summary>
    ///   Data source.
    /// </summary>
    public DataSource DataSource { get; }

    /// <summary>
    ///   Board.
    /// </summary>
    public IBoard Board { get; }
    #endregion

    #region public methods
    /// <summary>
    ///   Get list of trades.
    /// </summary>
    public List<ITrade> GetTrades(DateTime from, DateTime till)
    {
      return moExClient.GetTrades(this, from, till, 0);
    }

    /// <summary>
    ///   Get list of trades.
    /// </summary>
    public List<ITrade> GetTrades(int count, int offset)
    {
      return moExClient.GetTrades(
        this,
        TradeDataExistsFrom.Value,
        TradeDataExistsTill.Value,
        offset);
    }

    /// <summary>
    ///   Get count of trades.
    /// </summary>
    public int GetTradesCount()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Update data exists dates.
    /// </summary>
    public void UpdateDataExistsDates()
    {
      var dates = moExClient.GetSecurityDates(this);

      TradeDataExistsFrom = dates.Item1;
      TradeDataExistsTill = dates.Item2;
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="Security"> class.
    /// </summary>
    public Security(IMoExClient moExClient, IBoard board)
    {
      this.moExClient = moExClient;
      this.Board = board;
      this.DataSource = DataSource.OnlineMoscowExchange;
    }
    #endregion
  }
}
