using System;
using Newtonsoft.Json;

namespace Jira.Api.Infrastructure.Models
{
    public class JStatusItem
    {
        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("iconUrl")]
        public Uri IconUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("statusCategory")]
        public JStatusCategoryItem StatusCategory { get; set; }
    }
}