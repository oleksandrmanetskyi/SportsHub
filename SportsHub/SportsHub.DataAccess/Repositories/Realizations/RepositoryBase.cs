using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SportsHub.DataAccess.Repositories.Interfaces;

namespace SportsHub.DataAccess.Repositories.Realizations
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected SportsHubDbContext DbContext { get; set; }

        public RepositoryBase(SportsHubDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task CreateAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            this.DbContext.Set<T>().Update(entity); 
            DbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            this.DbContext.Set<T>().Remove(entity);
            DbContext.SaveChanges();
        }

        public IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var query = DbContext.Set<T>().AsNoTracking();
            if (include != null)
            {
                query = include(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return query;
        }
    }
}
