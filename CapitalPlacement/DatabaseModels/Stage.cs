using Newtonsoft.Json;

namespace CapitalPlacement.DatabaseModels
{
    public class Stage
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
        [JsonProperty("applicationId")]
        public string ApplicationId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
        [JsonProperty("data")]
        public StageData Data { get; set; } = new();
        [JsonProperty("isActive")]
        public bool IsActive { get; set; } = true;
        [JsonProperty("isHidden")]
        public bool IsHidden { get; set; } = false;
    }

    public class StageData
    {
        [JsonProperty("question")]
        public string Question { get; set; } = string.Empty;
        [JsonProperty("additionalInformation")]
        public string AdditionalInformation { get; set; } = string.Empty;
        [JsonProperty("duration")]
        public string Duration { get; set; } = string.Empty;
        [JsonProperty("unit")]
        public string Unit { get; set; } = string.Empty;
        [JsonProperty("deadline")]
        public string Deadline { get; set; } = string.Empty;
        [JsonProperty("isHidden")]
        public bool IsHidden { get; set; }
    }
}
