using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeHistoryDataService.Database.Models;

namespace TradeHistoryDataService.Database.Configurations
{
  /// <summary>
  ///   Securities table configuration.
  /// </summary>
  internal class SecurityConfiguration : IEntityTypeConfiguration<DbSecurity>
  {
    /// <summary>
    ///   Configuration.
    /// </summary>
    public void Configure(EntityTypeBuilder<DbSecurity> builder)
    {
      builder.HasKey(s => s.Id);
      builder.Property(s => s.Name).IsRequired().HasMaxLength(20);
      builder.Property(s => s.Description).IsRequired(false).HasMaxLength(200);
      builder.Property(s => s.TradeDataExistsFrom).IsRequired(false).HasColumnType("date");
      builder.Property(s => s.TradeDataExistsTill).IsRequired(false).HasColumnType("date");

      builder.HasMany(s => s.Trades)
        .WithOne(c => c.Security)
        .HasForeignKey(p => p.SecurityId);

      builder.HasOne(s => s.Board)
        .WithMany(c => c.Securities)
        .HasForeignKey(p => p.BoardId);
    }
  }
}
