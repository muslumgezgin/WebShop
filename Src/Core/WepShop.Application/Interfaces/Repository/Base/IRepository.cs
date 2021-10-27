using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WepShop.Application.Wrappers;
using WepShop.Domain.Common;

namespace WepShop.Application.Interfaces.Repository.Base
{
    public interface IRepository <T> where  T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<PagedResponse<IEnumerable<T>>> FindAsync (Expression<Func<T, bool>> filter=null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,  
            string includeProperties=null, int? pageIndex=null, int? pageSize=null);

        Task<T> GetByIdAsync(Guid Id);

        Task<T> AddAsync(T entity);

        Task AddRangeAsync (IEnumerable<T> entities);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task DeleteRangeAsync(IEnumerable<T> entities);

    }
}