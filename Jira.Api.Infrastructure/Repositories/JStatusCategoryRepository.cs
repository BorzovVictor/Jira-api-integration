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
    public interface IJStatusCategoryRepository : IBaseRepository<JStatusCategory>
    {
        Task<JStatusCategory> GetById(int id, CancellationToken token = default);
    }

    public class JStatusCategoryRepository : IJStatusCategoryRepository
    {
        private readonly IGenericRepository<JStatusCategory> _repository;
        private readonly IJStatusCategoryService _categoryService;

        public JStatusCategoryRepository(IJStatusCategoryService categoryService,
            IGenericRepository<JStatusCategory> repository)
        {
            _categoryService = categoryService;
            _repository = repository;
        }

        public async Task<List<JStatusCategory>> GetAsync(Expression<Func<JStatusCategory, bool>> query = null,
            Func<IQueryable<JStatusCategory>, IOrderedQueryable<JStatusCategory>> orderBy = null,
            Func<IQueryable<JStatusCategory>, IIncludableQueryable<JStatusCategory, object>> include = null,
            bool disableTracking = true,
            CancellationToken token = default)
        {
            return await _repository.GetAsync(query, orderBy, include, disableTracking, token);
        }

        public async Task Sync(CancellationToken token = default)
        {
            var categories = await _categoryService.GetAsync(token);

            if (categories.IsNullOrEmpty())
                throw new Exception($"{nameof(categories)} for sync not found");

            var dbCategories = (await _repository.GetAsync(token: token)) ?? new List<JStatusCategory>();
            foreach (var category in categories)
            {
                var existCategory = dbCategories.FirstOrDefault(x => x.Id==category.Id);
                if (existCategory.IsNullOrEmpty())
                {
                    await _repository.InsertAsync(category);
                }
                else
                {
                    _repository.PatchEntity(category, existCategory);
                }
            }
            await _repository.SaveAsync(token);
        }

        public async Task<JStatusCategory> GetById(int id, CancellationToken token = default)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}