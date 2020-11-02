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
    public interface IJSprintService
    {
        Task<List<JSprint>> GetSprintsAsync(int boardId, CancellationToken token = default);
        Task<JSprint> GetSprintByIdAsync(long id, CancellationToken token = default);
    }
    
    public class JSprintService: JiraApi, IJSprintService
    {
        private readonly IMapper _mapper;
        public JSprintService(IOptions<JCredentials> credentials, IMapper mapper) : base(credentials)
        {
            _mapper = mapper;
        }

        public async Task<List<JSprint>> GetSprintsAsync(int boardId, CancellationToken token = default)
        {
            var sprintsJson = await _jira.RestClient.ExecuteRequestAsync<JSprintQuery>(Method.GET,
                $"{BaseAgileUrl}/board/{boardId}/sprint", null, token);
            var jSprintItems = sprintsJson.JSprints;
            if(jSprintItems==null || !jSprintItems.Any())
                return new List<JSprint>();

            return _mapper.Map<List<JSprint>>(jSprintItems);
        }

        public async Task<JSprint> GetSprintByIdAsync(long id, CancellationToken token = default)
        {
            var sprintJson = await _jira.RestClient.ExecuteRequestAsync<JSprintItem>(Method.GET,
                $"{BaseAgileUrl}/sprint/{id}", null, token);

            if (sprintJson.IsNullOrEmpty())
                return null;
            return _mapper.Map<JSprint>(sprintJson);
        }
    }

}