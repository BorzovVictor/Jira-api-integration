using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Jira.Api.Infrastructure.Extensions;
using Jira.Api.Infrastructure.Models;
using Jira.Api.Infrastructure.Options;
using Jira.Core.Data.Entities;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Jira.Api.Infrastructure
{
    public interface IJIssueCommentService
    {
        Task<List<JIssueComment>> GetByIdAsync(int id, CancellationToken token = default);
    }
    
    public class JIssueCommentService: JiraApi, IJIssueCommentService
    {
        private readonly IMapper _mapper;
        public JIssueCommentService(IOptions<JCredentials> credentials, IMapper mapper) : base(credentials)
        {
            _mapper = mapper;
        }
        
        public async Task<List<JIssueComment>> GetByIdAsync(int id, CancellationToken token = default)
        {
            var response = await _jira.RestClient.ExecuteRequestAsync<JIssueCommentQuery>(Method.GET,
                $"{BaseUrl}/issue/{id}/comment", null, token);
            if(response.IsNullOrEmpty() || !response.Comments.Any())
                return new List<JIssueComment>();
            return _mapper.Map<List<JIssueComment>>(response.Comments);
        }

    }

}