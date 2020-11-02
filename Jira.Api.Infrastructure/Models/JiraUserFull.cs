using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Jira.Api.Infrastructure.Models
{
 public partial class JiraUserFull
    {
        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("accountType")]
        public string AccountType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avatarUrls")]
        public AvatarUrls AvatarUrls { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("groups")]
        public ApplicationRoles Groups { get; set; }

        [JsonProperty("applicationRoles")]
        public ApplicationRoles ApplicationRoles { get; set; }

        [JsonProperty("expand")]
        public string Expand { get; set; }
        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }
    }

    public partial class ApplicationRoles
    {
        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("items")]
        public List<object> Items { get; set; }
    }
    
    public partial class JiraUserFull
    {
        public static JiraUserFull FromJson(string json) => JsonConvert.DeserializeObject<JiraUserFull>(json, Jira.Api.Infrastructure.Models.ConverterJiraUserEx.Settings);
    }

    public static class SerializeJiraUserEx
    {
        public static string ToJson(this JiraUserFull self) => JsonConvert.SerializeObject(self, Jira.Api.Infrastructure.Models.ConverterJiraUserEx.Settings);
    }

    internal static class ConverterJiraUserEx
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}