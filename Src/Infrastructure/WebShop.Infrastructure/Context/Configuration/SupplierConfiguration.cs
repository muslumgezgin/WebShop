using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WepShop.Domain.Entities;

namespace WebShop.Infrastructure.Context.Configuration
{
    public class SupplierConfiguration:IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");

            builder.Property(s => s.Id)
                .HasColumnName("id")
                .HasValueGenerator<GuidValueGenerator>()
                .IsRequired();

            builder.Property(s => s.supplierName)
                .HasColumnName("supplier_name");
        }
    }
}