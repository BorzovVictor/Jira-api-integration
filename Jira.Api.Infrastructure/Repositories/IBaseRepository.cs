using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Jira.Api.Infrastructure
{
    public interface IBaseRepository<TEntity>
    {
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> query = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, CancellationToken token = default);
        Task Sync(CancellationToken token = default);
    }
}