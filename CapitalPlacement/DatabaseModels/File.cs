using Newtonsoft.Json;

namespace CapitalPlacement.DatabaseModels
{
    public class File
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty("contentType")]
        public string ContentType { get; set; } = string.Empty;
        [JsonProperty("value")]
        public string Value { get; set; } = string.Empty;
        [JsonProperty("isActive")]
        public bool IsActive { get; set; } = true;
    }
}
