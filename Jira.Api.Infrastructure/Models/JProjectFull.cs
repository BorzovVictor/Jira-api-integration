using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Jira.Api.Infrastructure.Models
{
public partial class JProjectFull
    {
        [JsonProperty("expand")]
        public string Expand { get; set; }

        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("lead")]
        public Lead Lead { get; set; }

        [JsonProperty("components")]
        public List<Component> Components { get; set; }

        [JsonProperty("issueTypes")]
        public List<IssueType> IssueTypes { get; set; }

        [JsonProperty("assigneeType")]
        public string AssigneeType { get; set; }

        [JsonProperty("versions")]
        public List<object> Versions { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("roles")]
        public Roles Roles { get; set; }

        [JsonProperty("avatarUrls")]
        public AvatarUrls AvatarUrls { get; set; }

        [JsonProperty("projectTypeKey")]
        public string ProjectTypeKey { get; set; }

        [JsonProperty("simplified")]
        public bool Simplified { get; set; }

        [JsonProperty("style")]
        public string Style { get; set; }

        [JsonProperty("isPrivate")]
        public bool IsPrivate { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }
    }

    public partial class Component
    {
        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isAssigneeTypeValid")]
        public bool IsAssigneeTypeValid { get; set; }
    }

    public partial class IssueType
    {
        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("iconUrl")]
        public Uri IconUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("subtask")]
        public bool Subtask { get; set; }

        [JsonProperty("avatarId", NullValueHandling = NullValueHandling.Ignore)]
        public long? AvatarId { get; set; }
    }

    public partial class Lead
    {
        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avatarUrls")]
        public AvatarUrls AvatarUrls { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }
    }

    public partial class Properties
    {
    }

    public partial class Roles
    {
        [JsonProperty("atlassian-addons-project-access")]
        public Uri AtlassianAddonsProjectAccess { get; set; }

        [JsonProperty("Project lead")]
        public Uri ProjectLead { get; set; }

        [JsonProperty("Developers")]
        public Uri Developers { get; set; }

        [JsonProperty("Customers")]
        public Uri Customers { get; set; }

        [JsonProperty("Team members")]
        public Uri TeamMembers { get; set; }

        [JsonProperty("Administrators")]
        public Uri Administrators { get; set; }
    }

    public partial class JProjectFull
    {
        public static JProjectFull FromJson(string json) => JsonConvert.DeserializeObject<JProjectFull>(json, JProjectFullConverter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this JProjectFull self) => JsonConvert.SerializeObject(self, JProjectFullConverter.Settings);
    }

    internal static class JProjectFullConverter
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

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}