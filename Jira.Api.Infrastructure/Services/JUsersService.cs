using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlassian.Jira;
using AutoMapper;
using Jira.Api.Infrastructure.Extensions;
using Jira.Api.Infrastructure.Models;
using Jira.Api.Infrastructure.Options;
using Jira.Core.Data.Entities;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Jira.Api.Infrastructure
{
    public interface IJUsersService
    {
        Task<JiraUser> GetUserByNameAsync(string username, CancellationToken token = default(CancellationToken));
        Task<JiraUserFull> GetUserByKeyAsync(string key, CancellationToken token = default(CancellationToken));
        Task<List<JiraUser>> GetUsersAsync(CancellationToken token = default(CancellationToken));
        Task<List<JUser>> GetUsersFullAsync(JiraUserFilter filter,CancellationToken token = default(CancellationToken));
    }
    public class JUsersService: JiraApi, IJUsersService
    {
        private readonly IMapper _mapper;
        public JUsersService(IOptions<JCredentials> credentials, IMapper mapper) : base(credentials)
        {
            _mapper = mapper;
        }
        
        public async Task<JiraUser> GetUserByNameAsync(string username,
            CancellationToken token = default(CancellationToken))
        {
            return await _jira.RestClient.ExecuteRequestAsync<JiraUser>(Method.GET,
                $"{BaseUrl}/user?username={(object) Uri.EscapeDataString(username)}", null, token);
        }
        
        public async Task<JiraUserFull> GetUserByKeyAsync(string key,
            CancellationToken token = default(CancellationToken))
        {
            return await _jira.RestClient.ExecuteRequestAsync<JiraUserFull>(Method.GET,
                $"{BaseUrl}/user?key={(object) Uri.EscapeDataString(key)}", null, token);
        }

        public async Task<List<JiraUser>> GetUsersAsync(CancellationToken token = default(CancellationToken))
        {
            var users = await _jira.Users.SearchUsersAsync("", JiraUserStatus.Active, maxResults: 200, token: token);
            return users.ToList();
        }
        
        public Task<IEnumerable<JiraUser>> GetAllUsersAsync(JiraUserFilter filter, CancellationToken token = default)
        {
            var url =
                $"rest/api/2/user/search?username=&includeActive=True&includeInactive=True&startAt=0&maxResults={filter.MaxResult}";
            return this._jira.RestClient.ExecuteRequestAsync<IEnumerable<JiraUser>>(
                Method.GET, url, (object) null, token
            );
        }

        public async Task<List<JUser>> GetUsersFullAsync(JiraUserFilter filter, CancellationToken token = default)
        {
            var result = new List<JUser>();

            var users = await GetAllUsersAsync(filter, token);
            foreach (var user in users)
            {
                var userFull = await GetUserByKeyAsync(user.Key, token);
                if (userFull.IsNullOrEmpty()) continue;
                var jUser = _mapper.Map<JUser>(userFull);
                result.Add(jUser);
            }

            return result;
        }

    }
}