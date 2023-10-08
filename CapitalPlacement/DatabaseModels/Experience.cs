using Newtonsoft.Json;

namespace CapitalPlacement.DatabaseModels
{
    public class Experience
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
        [JsonProperty("school")]
        public string School { get; set; } = string.Empty;
        [JsonProperty("degree")]
        public string Degree { get; set; } = string.Empty;
        [JsonProperty("courseName")]
        public string CourseName { get; set; } = string.Empty;
        [JsonProperty("location")]
        public string Location { get; set; } = string.Empty;
        [JsonProperty("startDate")]
        public string StartDate { get; set; } = string.Empty;
        [JsonProperty("endDate")]
        public string EndDate { get; set; } = string.Empty;
        [JsonProperty("isCurrent")]
        public bool IsCurrent { get; set; }
        [JsonProperty("applicationId")]
        public string ApplicationId { get; set; } = string.Empty;
        [JsonProperty("isEducation")]
        public bool IsEducation { get; set; }
        [JsonProperty("company")]
        public string Company { get; set; } = string.Empty;
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;
        [JsonProperty("isActive")]
        public bool IsActive { get; set; } = true;
    }
}
