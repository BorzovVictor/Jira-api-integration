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
    public interface IJStatusCategoryService
    {
        Task<List<JStatusCategory>> GetAsync(CancellationToken token = default);
        Task<JStatusCategory> GetByIdAsync(int id, CancellationToken token = default);
    }
    public class JStatusCategoryService: JiraApi, IJStatusCategoryService
    {
        public JStatusCategoryService(IOptions<JCredentials> credentials) : base(credentials)
        {
        }

        public async Task<List<JStatusCategory>> GetAsync(CancellationToken token = default)
        {
            var categories = await _jira.RestClient.ExecuteRequestAsync<List<JStatusCategory>>(Method.GET,
                $"{BaseUrl}/statuscategory", null, token);

            if(categories==null || !categories.Any())
                return new List<JStatusCategory>();

            return categories;
        }

        public async Task<JStatusCategory> GetByIdAsync(int id, CancellationToken token = default)
        {
            var category = await _jira.RestClient.ExecuteRequestAsync<JStatusCategory>(Method.GET,
                $"{BaseUrl}/statuscategory/{id}", null, token);
            return category;
        }
    }

}