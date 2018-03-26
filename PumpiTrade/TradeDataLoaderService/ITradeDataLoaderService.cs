using MetaData.Models.TradeDataModels;

namespace TradeDataLoaderService
{
  /// <summary>
  ///   Trade data loader service.
  /// </summary>
  public interface ITradeDataLoaderService
  {
    /// <summary>
    ///   Fetch data.
    /// </summary>
    void FetchData();

    /// <summary>
    ///   Download trade data for the given security.
    /// </summary>
    void DownloadTradeData(ISecurity security);

    /// <summary>
    ///   Update trade data.
    /// </summary>
    void FetchTradeData();
  }
}
