using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class WalletTypeMap : IEntityTypeConfiguration<WalletType>
    {
        public void Configure(EntityTypeBuilder<WalletType> builder)
        {
            builder.ToTable("WalletTypes");

            builder.HasKey(w => w.Id);

            builder.Property(w => w.Id)
                .HasMaxLength(36);

            builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(60);
        }
    }
}