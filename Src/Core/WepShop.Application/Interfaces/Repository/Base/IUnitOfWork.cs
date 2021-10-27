using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace WepShop.Application.Interfaces.Repository.Base
{
    public interface IUnitOfWork :IDisposable,IAsyncDisposable
    {
        IProductRepository _productRepository { get; }
        
        ISupplierRepository _supplierRepository { get; }
        
        IBrandRepository _brandRepository { get; }
        
        IBrandModelRepository _brandModelRepository { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<int> CommitAsync();
    }
}