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
    public interface IJIssueTypeService
    {
        Task<List<JIssueType>> GetAsync(CancellationToken token = default);
        Task<JIssueType> GetByIdAsync(int id, CancellationToken token = default);
    }
    
    public class JIssueTypeService: JiraApi, IJIssueTypeService
    {
        public JIssueTypeService(IOptions<JCredentials> credentials) : base(credentials)
        {
        }


        public async Task<List<JIssueType>> GetAsync(CancellationToken token = default)
        {
            var issueTypes = await _jira.RestClient.ExecuteRequestAsync<List<JIssueType>>(Method.GET,
                $"{BaseUrl}/issuetype", null, token);

            if(issueTypes==null || !issueTypes.Any())
                return new List<JIssueType>();

            return issueTypes;
        }

        public async Task<JIssueType> GetByIdAsync(int id, CancellationToken token = default)
        {
            var issueType = await _jira.RestClient.ExecuteRequestAsync<JIssueType>(Method.GET,
                $"{BaseUrl}/issuetype/{id}", null, token);
            return issueType;
        }
    }

}