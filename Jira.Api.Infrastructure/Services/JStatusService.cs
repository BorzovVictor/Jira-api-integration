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
    public interface IJStatusService
    {
        Task<List<JStatus>> GetAsync(CancellationToken token = default);
        Task<JStatus> GetByIdAsync(int id, CancellationToken token = default);
    }
    public class JStatusService: JiraApi, IJStatusService
    {
        private readonly IMapper _mapper;
        public JStatusService(IOptions<JCredentials> credentials, IMapper mapper) : base(credentials)
        {
            _mapper = mapper;
        }

        public async Task<List<JStatus>> GetAsync(CancellationToken token = default)
        {
            var statuses = await _jira.RestClient.ExecuteRequestAsync<List<JStatusItem>>(Method.GET,
                $"{BaseUrl}/status", null, token);

            if(statuses==null || !statuses.Any())
                return new List<JStatus>();

            return _mapper.Map<List<JStatus>>(statuses);
        }

        public async Task<JStatus> GetByIdAsync(int id, CancellationToken token = default)
        {
            var status = await _jira.RestClient.ExecuteRequestAsync<JStatusItem>(Method.GET,
                $"{BaseUrl}/status/{id}", null, token);
            return _mapper.Map<JStatus>(status);
        }
    }

}