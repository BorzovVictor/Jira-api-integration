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
    public interface IJStatusRepository : IBaseRepository<JStatus>
    {
    }

    public class JStatusRepository : IJStatusRepository
    {
        private readonly IGenericRepository<JStatus> _repository;
        private readonly IJStatusService _statusService;
        private readonly IJStatusCategoryRepository _statusCategoryRepo;

        public JStatusRepository(IGenericRepository<JStatus> repository, IJStatusService statusService,
            IJStatusCategoryRepository statusCategoryRepo)
        {
            _repository = repository;
            _statusService = statusService;
            _statusCategoryRepo = statusCategoryRepo;
        }

        public async Task<List<JStatus>> GetAsync(Expression<Func<JStatus, bool>> query = null,
            Func<IQueryable<JStatus>, IOrderedQueryable<JStatus>> orderBy = null,
            Func<IQueryable<JStatus>, IIncludableQueryable<JStatus, object>> include = null,
            bool disableTracking = true,
            CancellationToken token = default)
        {
            return await _repository.GetAsync(query, orderBy, include, disableTracking, token);
        }

        public async Task Sync(CancellationToken token = default)
        {
            var statuses = await _statusService.GetAsync(token);

            if (statuses.IsNullOrEmpty())
                throw new Exception($"{nameof(statuses)} for sync not found");

            var dbStatuses = (await _repository.GetAsync(token: token)) ?? new List<JStatus>();
            foreach (var status in statuses)
            {
                var existStatus = dbStatuses.FirstOrDefault(x => x.Id == status.Id);
                if (existStatus.IsNullOrEmpty())
                {
                    status.JStatusCategoryId = status.StatusCategory.Id;
                    var category = await _statusCategoryRepo.GetById(status.StatusCategory.Id, token);
                    if (!category.IsNullOrEmpty())
                        status.StatusCategory = category;
                    await _repository.InsertAsync(status);
                }
                else
                {
                    _repository.PatchEntity(status, existStatus);
                }
            }

            await _repository.SaveAsync(token);
        }
    }
}