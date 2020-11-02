using Jira.Core.Data.Entities;

namespace Jira.Api.Infrastructure.Models
{
    public class JIssueComment
    {
        public long Id { get; set; }
        public string Self { get; set; }
        public JUser Author { get; set; }
        public string Body { get; set; }
        public JUser UpdateAuthor { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public bool JsdPublic { get; set; }
    }
}