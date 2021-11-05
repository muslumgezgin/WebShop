using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WepShop.Application.Interfaces.Context;
using WepShop.Domain.Common;
using WepShop.Domain.Entities;

namespace WebShop.Infrastructure.Context
{
    public class ApplicationDBContext : DbContext,IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandModel> BrandModels { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
        }

        public override async  Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.Now;
                        break;
                }
            }
            
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}