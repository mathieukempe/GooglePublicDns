using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GooglePublicDns
{
    public class Answer
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RecordType Type { get; set; }

        // ReSharper disable once InconsistentNaming
        public int TTL { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
