using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeHistoryDataService.Database.Models;

namespace TradeHistoryDataService.Database.Configurations
{
  /// <summary>
  ///   Engines table configuration.
  /// </summary>
  internal class EngineConfiguration : IEntityTypeConfiguration<DbEngine>
  {
    /// <summary>
    ///   Configuration.
    /// </summary>
    public void Configure(EntityTypeBuilder<DbEngine> builder)
    {
      builder.HasKey(s => s.Id);
      builder.Property(s => s.Description).IsRequired(false).HasMaxLength(200);
      builder.Property(s => s.Name).IsRequired().HasMaxLength(20);

      builder.HasMany(s => s.Markets)
        .WithOne(c => c.Engine)
        .HasForeignKey(p => p.EngineId);
    }
  }
}
