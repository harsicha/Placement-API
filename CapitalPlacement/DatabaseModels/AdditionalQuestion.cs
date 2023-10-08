using Newtonsoft.Json;

namespace CapitalPlacement.DatabaseModels
{
    public class AdditionalQuestion
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
        [JsonProperty("question")]
        public string Question { get; set; } = string.Empty;
        [JsonProperty("choices")]
        public List<Choice> Choices { get; set; } = new();
        [JsonProperty("isOther")]
        public bool IsOther { get; set; }
        [JsonProperty("maxChoiceAllowed")]
        public int MaxChoiceAllowed { get; set; }
        [JsonProperty("isDisqualified")]
        public bool IsDisqualified { get; set; }
        [JsonProperty("answer")]
        public string Answer { get; set; } = string.Empty;
        [JsonProperty("isPersonal")]
        public bool IsPersonal { get; set; }
        [JsonProperty("isProfile")]
        public bool IsProfile { get; set; }
        [JsonProperty("isAdditional")]
        public bool IsAdditional { get; set; }
        [JsonProperty("fileId")]
        public string FileId { get; set; } = string.Empty;
        [JsonProperty("videoURL")]
        public string VideoURL { get; set; } = string.Empty;
        [JsonProperty("isActive")]
        public bool IsActive { get; set; } = true;
        [JsonProperty("applicationId")]
        public string ApplicationId { get; set; } = string.Empty;
    }

    public class Choice
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
        [JsonProperty("value")]
        public string Value { get; set; } = string.Empty;
    }
}
