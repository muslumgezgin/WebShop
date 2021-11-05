using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WepShop.Domain.Entities;

namespace WebShop.Infrastructure.Context.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");

            builder.Property(s => s.Id)
                .HasColumnName("id")
                // For Inmemory
                .HasValueGenerator<GuidValueGenerator>()
                // For Postgresql
                // .UseIdentityColumn<Guid>()
                .IsRequired();

            builder.Property(s => s.brandName)
                .HasColumnName("brand_name")
                .IsRequired();
            
        }
    }
}