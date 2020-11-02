using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Jira.Api.Infrastructure.Models
{
public partial class JBoardQuery
    {
        [JsonProperty("maxResults")]
        public long MaxResults { get; set; }

        [JsonProperty("startAt")]
        public long StartAt { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("isLast")]
        public bool IsLast { get; set; }

        [JsonProperty("values")]
        public List<JBoardItem> BoardItems { get; set; }
    }

    public partial class JBoardItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("projectId")]
        public long ProjectId { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("projectName")]
        public string ProjectName { get; set; }

        [JsonProperty("projectKey")]
        public string ProjectKey { get; set; }

        [JsonProperty("projectTypeKey")]
        public string ProjectTypeKey { get; set; }

        [JsonProperty("avatarURI")]
        public string AvatarUri { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class JBoardQuery
    {
        public static JBoardQuery FromJson(string json) => JsonConvert.DeserializeObject<JBoardQuery>(json, JBoardQueryConverter.Settings);
    }

    public static class JBoardQuerySerialize
    {
        public static string ToJson(this JBoardQuery self) => JsonConvert.SerializeObject(self, JBoardQueryConverter.Settings);
    }

    internal static class JBoardQueryConverter
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