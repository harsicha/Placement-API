using CapitalPlacement.DatabaseModels;
using Newtonsoft.Json;

namespace CapitalPlacement.DataTransferModels
{
    public class ProgramDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
        [JsonProperty("programTitle")]
        public ValueObject ProgramTitle { get; set; } = new();
        [JsonProperty("programSummary")]
        public ValueObject ProgramSummary { get; set; } = new();
        [JsonProperty("programDescription")]
        public ValueObject ProgramDescription { get; set; } = new();
        [JsonProperty("keySkills")]
        public string[] KeySkills { get; set; } = Array.Empty<string>();
        [JsonProperty("programBenefits")]
        public ValueObject ProgramBenefits { get; set; } = new();
        [JsonProperty("applicationCriteria")]
        public ValueObject ApplicationCriteria { get; set; } = new();
        [JsonProperty("additionalInformation")]
        public AdditionalInformation AdditionalInformation { get; set; } = new();
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; } = string.Empty;
        [JsonProperty("modifiedBy")]
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
