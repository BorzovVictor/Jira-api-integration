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
    public interface IJIssueTypeRepository
    {
        Task<List<JIssueType>> GetAsync(Expression<Func<JIssueType, bool>> query = null,
            Func<IQueryable<JIssueType>, IOrderedQueryable<JIssueType>> orderBy = null,
            Func<IQueryable<JIssueType>, IIncludableQueryable<JIssueType, object>> include = null,
            bool disableTracking = true, CancellationToken token = default);
        Task Sync(CancellationToken token = default);
    }
    
    public class JIssueTypeRepository: IJIssueTypeRepository
    {
        private readonly IGenericRepository<JIssueType> _repository;
        private readonly IJIssueTypeService _issueTypeService;

        public JIssueTypeRepository(IJIssueTypeService sprintService, IGenericRepository<JIssueType> repository)
        {
            _issueTypeService = sprintService;
            _repository = repository;
        }

        public async Task<List<JIssueType>> GetAsync(Expression<Func<JIssueType, bool>> query = null,
            Func<IQueryable<JIssueType>, IOrderedQueryable<JIssueType>> orderBy = null,
            Func<IQueryable<JIssueType>, IIncludableQueryable<JIssueType, object>> include = null,
            bool disableTracking = true,
            CancellationToken token = default)
        {
            return await _repository.GetAsync(query, orderBy, include, disableTracking, token);
        }

        public async Task Sync(CancellationToken token = default)
        {
            var issueTypes = await _issueTypeService.GetAsync(token);

            foreach (var issueType in issueTypes)
            {
                var existIssueType = await _repository.GetByIdAsync(issueType.Id);
                if (existIssueType.IsNullOrEmpty())
                {
                    await _repository.InsertAsync(issueType);
                }
                else
                {
                    _repository.PatchEntity(issueType, existIssueType);
                }
            }

            await _repository.SaveAsync(token);
        }
    }

}