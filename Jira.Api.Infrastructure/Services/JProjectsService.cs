using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlassian.Jira;
using AutoMapper;
using Jira.Api.Infrastructure.Models;
using Jira.Api.Infrastructure.Options;
using Jira.Core.Data.Entities;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Jira.Api.Infrastructure
{
    public interface IJProjectService
    {
        Task<List<Project>> GetProjectsAsync(CancellationToken token = default (CancellationToken));
        Task<List<JProject>> GetFullProjectAsync(CancellationToken token = default(CancellationToken));
        Task<JProjectFull> GetProjectAsync(string projectKey, CancellationToken token = default (CancellationToken));
    }
    public class JProjectService: JiraApi, IJProjectService
    {
        private readonly IMapper _mapper;
        public JProjectService(IOptions<JCredentials> credentials, IMapper mapper) : base(credentials)
        {
            _mapper = mapper;
        }

        public async Task<List<Project>> GetProjectsAsync(CancellationToken token = default(CancellationToken))
        {
            return (await _jira.Projects.GetProjectsAsync(token)).ToList();
        }

        public async Task<List<JProject>> GetFullProjectAsync(CancellationToken token = default(CancellationToken))
        {
            var projects = await GetProjectsAsync(token);
            List<JProject> jProjects = new List<JProject>();
            foreach (var project in projects)
            {
                var jFullProject = await GetProjectAsync(project.Key, token);
                var jProject = _mapper.Map<JProject>(jFullProject);
                jProjects.Add(jProject);
            }

            return jProjects;
        }

        public async Task<JProjectFull> GetProjectAsync(string projectKey, CancellationToken token = default(CancellationToken))
        {
            return await _jira.RestClient.ExecuteRequestAsync<JProjectFull>(Method.GET,
                $"{BaseUrl}/project/{projectKey}?expand=lead,url,description", null, token);
        }
    }

}