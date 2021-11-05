using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WepShop.Domain.Entities;

namespace WebShop.Infrastructure.Context.Configuration
{
    public class BrandModelConfiguration : IEntityTypeConfiguration<BrandModel>
    {
        public void Configure(EntityTypeBuilder<BrandModel> builder)
        {
            builder.ToTable("BrandModels");

            builder.Property(s => s.Id)
                .HasColumnName("id")
                .HasValueGenerator<GuidValueGenerator>()
                .IsRequired();

            builder.Property(s => s.brandId)
                .HasColumnName("brand_id")
                .IsRequired();

            builder.Property(s => s.modelName)
                .HasColumnName("model_name")
                .IsRequired();
            
        }
    }
}