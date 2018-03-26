using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeHistoryDataService.Database.Models;

namespace TradeHistoryDataService.Database.Configurations
{
  /// <summary>
  ///   Markets table configuration.
  /// </summary>
  internal class MarketConfiguration : IEntityTypeConfiguration<DbMarket>
  {
    /// <summary>
    ///   Configuration.
    /// </summary>
    public void Configure(EntityTypeBuilder<DbMarket> builder)
    {
      builder.HasKey(s => s.Id);
      builder.Property(s => s.Name).IsRequired().HasMaxLength(20);
      builder.Property(s => s.Description).IsRequired(false).HasMaxLength(200);

      builder.HasOne(s => s.Engine)
        .WithMany(c => c.Markets)
        .HasForeignKey(p => p.EngineId);

      builder.HasMany(s => s.Boards)
        .WithOne(c => c.Market)
        .HasForeignKey(p => p.MarketId);
    }
  }
}
