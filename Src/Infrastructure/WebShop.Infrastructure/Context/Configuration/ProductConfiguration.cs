using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WepShop.Domain.Entities;

namespace WebShop.Infrastructure.Context.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.Property(s => s.Id)
                .HasColumnName("id")
                .HasValueGenerator<GuidValueGenerator>()
                .IsRequired();
            
            builder.Property(s => s.brandModelId)
                .HasColumnName("brand_model_id")
                .IsRequired();
            
            builder.Property(s => s.supplerId)
                .HasColumnName("supplier_id")
                .IsRequired();
            builder.Property(s => s.productCode)
                .HasColumnName("product_code")
                .IsRequired();
            builder.Property(s => s.productName)
                .HasColumnName("product_name")
                .IsRequired();
        }
    }
}