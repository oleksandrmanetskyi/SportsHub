using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace SportsHub.DataAccess.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T:class
    {
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
    }
}
