using WebShop.Infrastructure.Context;
using WebShop.Infrastructure.Repositories.Base;
using WepShop.Application.Interfaces.Repository;
using WepShop.Domain.Entities;

namespace WebShop.Infrastructure.Repositories
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDBContext dbContext) : base(dbContext)
        {

        }
        
    }
}