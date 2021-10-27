using WebShop.Infrastructure.Context;
using WebShop.Infrastructure.Repositories.Base;
using WepShop.Application.Interfaces.Repository;
using WepShop.Domain.Entities;

namespace WebShop.Infrastructure.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDBContext dbContext) : base(dbContext)
        {

        }
        
    }
}