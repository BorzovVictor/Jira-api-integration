using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jira.Api.Infrastructure.Options;
using Jira.Core.Data.Entities;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Jira.Api.Infrastructure
{
    public interface IJPriorityService
    {
        Task<List<JPriority>> GetAsync(CancellationToken token = default);
        Task<JPriority> GetByIdAsync(int id, CancellationToken token = default);
    }
    public class JPriorityService: JiraApi, IJPriorityService
    {
        public JPriorityService(IOptions<JCredentials> credentials) : base(credentials)
        {
        }

        public async Task<List<JPriority>> GetAsync(CancellationToken token = default)
        {
            var priorities = await _jira.RestClient.ExecuteRequestAsync<List<JPriority>>(Method.GET,
                $"{BaseUrl}/priority", null, token);

            if(priorities==null || !priorities.Any())
                return new List<JPriority>();

            return priorities;
        }

        public async Task<JPriority> GetByIdAsync(int id, CancellationToken token = default)
        {
            var priority = await _jira.RestClient.ExecuteRequestAsync<JPriority>(Method.GET,
                $"{BaseUrl}/priority/{id}", null, token);
            return priority;
        }
    }

}