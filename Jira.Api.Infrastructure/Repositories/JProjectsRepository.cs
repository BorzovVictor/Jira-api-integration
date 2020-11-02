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
    public interface IJProjectsRepository
    {
        Task<List<JProject>> GetDbProjectsAsync(Expression<Func<JProject, bool>> query = null,
            Func<IQueryable<JProject>, IOrderedQueryable<JProject>> orderBy = null,
            Func<IQueryable<JProject>, IIncludableQueryable<JProject, object>> include = null,
            bool disableTracking = true, CancellationToken token = default);
        Task SyncProjects(CancellationToken token = default);
    }

    public class JProjectsRepository : IJProjectsRepository
    {
        private readonly IJProjectService _projectService;
        private readonly IGenericRepository<JProject> _repository;

        public JProjectsRepository(IJProjectService projectService,
            IGenericRepository<JProject> repository)
        {
            _projectService = projectService;
            _repository = repository;
        }

        public async Task<List<JProject>> GetDbProjectsAsync(Expression<Func<JProject, bool>> query = null,
            Func<IQueryable<JProject>, IOrderedQueryable<JProject>> orderBy = null,
            Func<IQueryable<JProject>, IIncludableQueryable<JProject, object>> include = null,
            bool disableTracking = true,
            CancellationToken token = default)
        {
            return await _repository.GetAsync(query, orderBy, include, disableTracking, token);
        }

        public async Task SyncProjects(CancellationToken token = default)
        {
            var jiraProjects = await _projectService.GetFullProjectAsync(token);
            if (jiraProjects.IsNullOrEmpty())
                throw new Exception("jira projects not founded");
            
            var dbProjects = await _repository.GetAsync(token: token);

            foreach (var project in jiraProjects)
            {
                var existDbProject = dbProjects.FirstOrDefault(x => x.Id == project.Id);
                if (existDbProject.IsNullOrEmpty())
                {
                    await _repository.InsertAsync(project);
                }
                else
                {
                    UpdateProject(project, existDbProject);
                }
            }
            await _repository.SaveAsync(token);
        }

        private void UpdateProject(JProject project, JProject existDbProject)
        {
            foreach (var property in existDbProject.GetType().GetProperties())
            {
                var oldValue = property.GetValue(existDbProject);
                var newValue = property.GetValue(project);

                if (Equals(oldValue, newValue)) continue;

                property.SetValue(existDbProject, newValue);
            }
        }
    }
}