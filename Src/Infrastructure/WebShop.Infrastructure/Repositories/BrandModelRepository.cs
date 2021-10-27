using WebShop.Infrastructure.Context;
using WebShop.Infrastructure.Repositories.Base;
using WepShop.Application.Interfaces.Repository;
using WepShop.Domain.Entities;

namespace WebShop.Infrastructure.Repositories
{
    public class BrandModelRepository : Repository<BrandModel>,IBrandModelRepository
    {
        public BrandModelRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            
        }
        
    }
}