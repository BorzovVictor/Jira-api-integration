using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jira.Api.Infrastructure.Models
{
    public class JIssueWorklogQuery
    {
        [JsonProperty("startAt")]
        public long StartAt { get; set; }

        [JsonProperty("maxResults")]
        public long MaxResults { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("worklogs")]
        public List<JIssueWorklogItem> Worklogs { get; set; }
    }
    
    public partial class JIssueWorklogItem
    {
        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("updateAuthor")]
        public Author UpdateAuthor { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("started")]
        public string Started { get; set; }

        [JsonProperty("timeSpent")]
        public string TimeSpent { get; set; }

        [JsonProperty("timeSpentSeconds")]
        public long TimeSpentSeconds { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("issueId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long IssueId { get; set; }
    }
}