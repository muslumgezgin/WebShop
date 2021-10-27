using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using WebShop.Infrastructure.Context;
using WepShop.Application.Interfaces.Context;
using WepShop.Application.Interfaces.Repository;
using WepShop.Application.Interfaces.Repository.Base;

namespace WebShop.Infrastructure.Repositories.Base
{
    public class UnitOfWork: IUnitOfWork
    {
    
        
        private IApplicationDbContext applicationDbContext;
        public IProductRepository _productRepository { get; }
        public ISupplierRepository _supplierRepository { get; }
        public IBrandRepository _brandRepository { get; }
        public IBrandModelRepository _brandModelRepository { get; }

        public UnitOfWork(IApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            _productRepository = new ProductRepository((ApplicationDBContext)this.applicationDbContext);
            _supplierRepository = new SupplierRepository((ApplicationDBContext)this.applicationDbContext);
            _brandRepository = new BrandRepository((ApplicationDBContext)this.applicationDbContext);
            _brandModelRepository = new BrandModelRepository((ApplicationDBContext)this.applicationDbContext);
        }

        public async  Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return  await ((ApplicationDBContext)this.applicationDbContext).Database.BeginTransactionAsync();
        }

        public async  Task<int> CommitAsync()
        {
            return await this.applicationDbContext.SaveChangesAsync();
        }
        
        public void Dispose()
        {
           ((ApplicationDBContext)this.applicationDbContext).Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await ((ApplicationDBContext)this.applicationDbContext).DisposeAsync();
        }
    }
}