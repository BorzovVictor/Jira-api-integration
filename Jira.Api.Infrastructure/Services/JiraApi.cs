using Jira.Api.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace Jira.Api.Infrastructure
{
    public class JiraApi
    {
        protected readonly Atlassian.Jira.Jira _jira;
        protected static string BaseUrl => "rest/api/2";
        protected static string BaseAgileUrl => "/rest/agile/1.0";
        public JiraApi(IOptions<JCredentials> credentials)
        {
            var cred = credentials;
            _jira = Atlassian.Jira.Jira.CreateRestClient(
                cred.Value.Url,
                cred.Value.UserName,
                cred.Value.Password
            );
        }
    }
}