using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test_Project.Database.Tables;

namespace Test_Project.Database.Configurations
{
    class PhoneConfiguration : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.HasKey(x => x.Model).HasName("phones_pkey");
            builder.ToTable("phones");

            builder.Property(x => x.Model).HasColumnName("model");
            builder.Property(x => x.Manufacturer).HasColumnName("manufacturer");
            builder.Property(x => x.Price).HasColumnName("price");

            builder.HasOne(x => x.ManufacturerProperty)
                .WithMany()
                .HasForeignKey(x => x.Manufacturer)
                .HasConstraintName("phones_manufacturers_fkey")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}