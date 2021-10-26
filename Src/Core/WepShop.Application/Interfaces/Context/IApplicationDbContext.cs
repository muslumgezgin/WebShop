using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WepShop.Domain.Entities;

namespace WepShop.Application.Interfaces.Context
{
    public interface IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        
        public DbSet<Brand> B { get; set; }
        
        public DbSet<BrandModel> BrandModels { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}