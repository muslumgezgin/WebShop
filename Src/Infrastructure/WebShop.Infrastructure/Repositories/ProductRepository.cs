using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.Infrastructure.Context;
using WebShop.Infrastructure.Repositories.Base;
using WepShop.Application.Interfaces.Repository;
using WepShop.Application.Wrappers;
using WepShop.Domain.Entities;

namespace WebShop.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDBContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Product>> GetProductByCode(string code)
        {
            PagedResponse<IEnumerable<Product>> pagedResponse = await this.FindAsync(x => x.productCode.Contains(code));
            return pagedResponse.Data;
        }
    }
}