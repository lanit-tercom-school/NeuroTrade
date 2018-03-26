using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeHistoryDataService.Database.Models;

namespace TradeHistoryDataService.Database.Configurations
{
  /// <summary>
  ///   Boards table configuration.
  /// </summary>
  internal class BoardConfiguration : IEntityTypeConfiguration<DbBoard>
  {
    /// <summary>
    ///   Configuration.
    /// </summary>
    public void Configure(EntityTypeBuilder<DbBoard> builder)
    {
      builder.HasKey(s => s.Id);
      builder.Property(s => s.Id);
      builder.Property(s => s.Name).IsRequired().HasMaxLength(20);
      builder.Property(s => s.Description).IsRequired(false).HasMaxLength(200);

      builder.HasOne(s => s.Market)
        .WithMany(c => c.Boards)
        .HasForeignKey(p => p.MarketId);

      builder.HasMany(s => s.Securities)
        .WithOne(c => c.Board)
        .HasForeignKey(p => p.BoardId);
    }
  }
}
