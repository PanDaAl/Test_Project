using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test_Project.Database.Tables;

namespace Test_Project.Database.Configurations
{
    class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasKey(x => x.Name).HasName("manufacturers_pkey");
            builder.ToTable("manufacturers");

            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Country).HasColumnName("country");
        }
    }
}