using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Jira.Api.Infrastructure.Models
{
    public partial class JSprintQuery
    {
        [JsonProperty("maxResults")] public long MaxResults { get; set; }

        [JsonProperty("startAt")] public long StartAt { get; set; }

        [JsonProperty("total")] public long Total { get; set; }

        [JsonProperty("isLast")] public bool IsLast { get; set; }

        [JsonProperty("values")] public List<JSprintItem> JSprints { get; set; }
    }

    public partial class JSprintItem
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("self")] public Uri Self { get; set; }

        [JsonProperty("state")] public string State { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("startDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? StartDate { get; set; }

        [JsonProperty("endDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? EndDate { get; set; }

        [JsonProperty("completeDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CompleteDate { get; set; }

        [JsonProperty("originBoardId", NullValueHandling = NullValueHandling.Ignore)]
        public long? OriginBoardId { get; set; }

        [JsonProperty("goal")] public string Goal { get; set; }
    }

    public partial class JSprintQuery
    {
        public static JSprintQuery FromJson(string json) =>
            JsonConvert.DeserializeObject<JSprintQuery>(json, JSprintQueryConverter.Settings);
    }

    public static class JSprintQuerySerialize
    {
        public static string ToJson(this JSprintQuery self) =>
            JsonConvert.SerializeObject(self, JSprintQueryConverter.Settings);
    }

    internal static class JSprintQueryConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
            },
        };
    }
}