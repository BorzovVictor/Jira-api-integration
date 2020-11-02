using System;

namespace Jira.Core.Data.Entities
{
    public class JIssueWorklog
    {
        public long Id { get; set; }
        public virtual JUser Author { get; set; }
        public string AuthorKey { get; set; }
        public virtual JUser UpdateAuthor { get; set; }
        public string UpdateAuthorKey { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Started { get; set; }
        public string TimeSpent { get; set; }
        public int TimeSpentSeconds { get; set; }
        public long IssueId { get; set; }
        public string Self { get; set; }
    }
}