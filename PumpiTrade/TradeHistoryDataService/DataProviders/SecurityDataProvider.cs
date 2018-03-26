using System.Collections.Generic;
using System.Linq;
using MetaData.Models.TradeDataModels;
using Microsoft.EntityFrameworkCore;
using TradeHistoryDataService.Database;
using TradeHistoryDataService.Database.Models;
using TradeHistoryDataService.Models;

namespace TradeHistoryDataService.DataProviders
{
  /// <summary>
  ///   Security data provider.
  /// </summary>
  public class SecurityDataProvider
  {
    #region private fields
    /// <summary>
    ///   Database connection string.
    /// </summary>
    private readonly string connectionString;
    #endregion

    #region public methods
    /// <summary>
    ///   Get by id.
    /// </summary>
    public ISecurity GetById(int id)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      return new Security(connectionString, dbContext.Securities.Find(id));
    }

    /// <summary>
    ///   Get all.
    /// </summary>
    public List<ISecurity> GetAll()
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var securities = dbContext.Securities
        .Include(s => s.Board)
        .ThenInclude(s => s.Market)
        .ThenInclude(s => s.Engine)
        .ToList();

      return new List<ISecurity>(
        securities.Select(s => new Security(connectionString, s)).ToList());
    }

    /// <summary>
    ///   Get by board.
    /// </summary>
    /// <param name="board"></param>
    public List<ISecurity> GetByBoard(IBoard board)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var securities = dbContext.Securities
        .Where(s => s.BoardId == board.Id)
        .Include(s => s.Board)
        .ThenInclude(s => s.Market)
        .ThenInclude(s => s.Engine)
        .ToList();

      return new List<ISecurity>(
        securities.Select(s => new Security(connectionString, s)).ToList());
    }

    /// <summary>
    ///   Add security.
    /// </summary>
    /// <param name="security"></param>
    public ISecurity Add(ISecurity security)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      var dbSecurity = new DbSecurity()
      {
        Description = security.Description,
        BoardId = security.Board.Id,
        Name = security.Name,
        TradeDataExistsFrom = security.TradeDataExistsFrom,
        TradeDataExistsTill = security.TradeDataExistsTill
      };

      dbContext.Securities.Add(dbSecurity);
      dbContext.SaveChanges();

      return new Security(connectionString, dbSecurity);
    }

    /// <summary>
    ///   Add range of securities.
    /// </summary>
    public void AddRange(List<ISecurity> securities)
    {
      TradeHistoryDbContext dbContext =
        new TradeHistoryDbContext(connectionString);

      foreach (var security in securities)
      {
        var dbSecurity = new DbSecurity()
        {
          Description = security.Description,
          BoardId = security.Board.Id,
          Name = security.Name,
          TradeDataExistsFrom = security.TradeDataExistsFrom,
          TradeDataExistsTill = security.TradeDataExistsTill
        };

        dbContext.Securities.Add(dbSecurity);
      }

      dbContext.SaveChanges();
    }

    /// <summary>
    ///   Initializes a new instance of <see cref="SecurityDataProvider"/> class.
    /// </summary>
    /// <param name="connectionString"></param>
    public SecurityDataProvider(string connectionString)
    {
      this.connectionString = connectionString;
    }
    #endregion
  }
}
