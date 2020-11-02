using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Jira.Api.Infrastructure.Extensions;
using Jira.Core.Data.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Jira.Api.Infrastructure
{
    public interface IJPriorityRepository
    {
        Task<List<JPriority>> GetAsync(Expression<Func<JPriority, bool>> query = null,
            Func<IQueryable<JPriority>, IOrderedQueryable<JPriority>> orderBy = null,
            Func<IQueryable<JPriority>, IIncludableQueryable<JPriority, object>> include = null,
            bool disableTracking = true, CancellationToken token = default);
        Task Sync(CancellationToken token = default);
    }

    public class JPriorityRepository : IJPriorityRepository
    {
        private readonly IGenericRepository<JPriority> _repository;
        private readonly IJPriorityService _priorityService;

        public JPriorityRepository(IGenericRepository<JPriority> repository, IJPriorityService priorityService)
        {
            _repository = repository;
            _priorityService = priorityService;
        }

        public async Task<List<JPriority>> GetAsync(Expression<Func<JPriority, bool>> query = null,
            Func<IQueryable<JPriority>, IOrderedQueryable<JPriority>> orderBy = null,
            Func<IQueryable<JPriority>, IIncludableQueryable<JPriority, object>> include = null,
            bool disableTracking = true,
            CancellationToken token = default)
        {
            return await _repository.GetAsync(query, orderBy, include, disableTracking, token);
        }

        public async Task Sync(CancellationToken token = default)
        {
            var priorities = await _priorityService.GetAsync(token);

            if (priorities.IsNullOrEmpty())
                throw new Exception("priorities for sync not found");

            var dbPriorities = (await _repository.GetAsync(token: token)) ?? new List<JPriority>();
            foreach (var priority in priorities)
            {
                var existPriority = dbPriorities.FirstOrDefault(x => x.Id==priority.Id);
                if (existPriority.IsNullOrEmpty())
                {
                    await _repository.InsertAsync(priority);
                }
                else
                {
                    _repository.PatchEntity(priority, existPriority);
                }
            }
            await _repository.SaveAsync(token);
        }
    }

}