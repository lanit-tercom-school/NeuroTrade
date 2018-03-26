using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeHistoryDataService.Database.Models;

namespace TradeHistoryDataService.Database.Configurations
{
  /// <summary>
  ///   Trades table configuration.
  /// </summary>
  internal class TradeConfiguration : IEntityTypeConfiguration<DbTrade>
  {
    /// <summary>
    ///   Configuration.
    /// </summary>
    public void Configure(EntityTypeBuilder<DbTrade> builder)
    {
      builder.HasKey(s => s.Id);
      builder.Property(s => s.ClosingPrice).IsRequired();
      builder.Property(s => s.OpeningPrice).IsRequired();
      builder.Property(s => s.LowestPrice).IsRequired();
      builder.Property(s => s.HighestPrice).IsRequired();
      builder.Property(s => s.TradeDate).IsRequired().HasColumnType("date");
      builder.Property(s => s.TradesCount).IsRequired();

      builder.HasOne(s => s.Security)
        .WithMany(c => c.Trades)
        .HasForeignKey(p => p.SecurityId);
    }
  }
}
