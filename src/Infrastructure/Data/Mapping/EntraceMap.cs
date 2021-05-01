using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class EntranceMap : IEntityTypeConfiguration<Entrance>
    {
        public void Configure(EntityTypeBuilder<Entrance> builder)
        {
            builder.ToTable("Entrances");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasMaxLength(36);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Observation)
                .HasMaxLength(500);

            builder.Property(e => e.Type)
                .IsRequired();

            builder.Property(e => e.Ticker)
                .HasMaxLength(10);

            builder.Property(e => e.Value)
                .IsRequired();

            builder.HasOne(e => e.Wallet)
                .WithMany(w => w.Entrances)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Category)
                .WithMany(c => c.Entrances);
        }
    }
}
