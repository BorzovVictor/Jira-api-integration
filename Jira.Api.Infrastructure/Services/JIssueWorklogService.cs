using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Jira.Api.Infrastructure.Models;
using Jira.Api.Infrastructure.Options;
using Jira.Core.Data.Entities;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Jira.Api.Infrastructure
{
    public interface IJIssueWorklogService
    {
        Task<List<JIssueWorklog>> GetByIssueIdAsync(int issueId, CancellationToken token = default);
        Task<JIssueWorklog> GetByIdAsync(int workLogId, long issueId, CancellationToken token = default);
    }
    public class JIssueWorklogService: JiraApi, IJIssueWorklogService
    {
        private readonly IMapper _mapper;
        public JIssueWorklogService(IOptions<JCredentials> credentials, IMapper mapper) : base(credentials)
        {
            _mapper = mapper;
        }
        
        public async Task<List<JIssueWorklog>> GetByIssueIdAsync(int issueId, CancellationToken token = default)
        {
            var worklogQuery = await _jira.RestClient.ExecuteRequestAsync<JIssueWorklogQuery>(Method.GET,
                $"{BaseUrl}/issue/{issueId}/worklog", null, token);

            if(worklogQuery==null || !worklogQuery.Worklogs.Any())
                return new List<JIssueWorklog>();

            return _mapper.Map<List<JIssueWorklog>>(worklogQuery.Worklogs);
        }

        public async Task<JIssueWorklog> GetByIdAsync(int workLogId, long issueId, CancellationToken token = default)
        {
            var workLog = await _jira.RestClient.ExecuteRequestAsync<JIssueWorklogItem>(Method.GET,
                $"{BaseUrl}/issue/{issueId}/worklog/{workLogId}", null, token);
            return _mapper.Map<JIssueWorklog>(workLog);
        }

    }

}