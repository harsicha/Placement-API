using Newtonsoft.Json;

namespace CapitalPlacement.DatabaseModels
{
    public class Application
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
        [JsonProperty("personalInformation")]
        public PersonalInformation PersonalInformation { get; set; } = new();
        [JsonProperty("profile")]
        public Profile Profile { get; set; } = new();
        [JsonProperty("coverImageId")]
        public string CoverImageId { get; set; } = string.Empty;
        [JsonProperty("isActive")]
        public bool IsActive { get; set; } = true;
        [JsonProperty("isDraft")]
        public bool IsDraft { get; set; } = false;
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; } = string.Empty;
        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [JsonProperty("modifiedBy")]
        public string ModifiedBy { get; set; } = string.Empty;
        [JsonProperty("modifiedOn")]
        public DateTime ModifiedOn { get; set; } = DateTime.Now;

    }

    public class ValueObject
    {
        [JsonProperty("isRequired")]
        public bool IsRequired { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; } = string.Empty;
    }

    public class AdditionalInformation
    {
        [JsonProperty("programType")]
        public ValueObject? ProgramType { get; set; }
        [JsonProperty("programStart")]
        public ValueObject? ProgramStart { get; set; }
        [JsonProperty("applicationOpen")]
        public ValueObject? ApplicationOpen { get; set; }
        [JsonProperty("aplicationClose")]
        public ValueObject? AplicationClose { get; set; }
        [JsonProperty("duration")]
        public ValueObject? Duration { get; set; }
        [JsonProperty("programLocation")]
        public ValueObject? ProgramLocation { get; set; }
        [JsonProperty("minQual")]
        public ValueObject? MinQual { get; set; }
        [JsonProperty("maxApps")]
        public ValueObject? MaxApps { get; set; }
    }

    public class PersonalInformation
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; } = string.Empty;
        [JsonProperty("lastName")]
        public string LastName { get; set; } = string.Empty;
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;
        [JsonProperty("phone")]
        public InfoObject Phone { get; set; } = new();
        [JsonProperty("nationality")]
        public InfoObject Nationality { get; set; } = new();
        [JsonProperty("currentResidence")]
        public InfoObject CurrentResidence { get; set; } = new();
        [JsonProperty("idNumber")]
        public InfoObject IDNumber { get; set; } = new();
        [JsonProperty("dob")]
        public InfoObject DOB { get; set; } = new();
        [JsonProperty("gender")]
        public InfoObject Gender { get; set; } = new();
    }

    public class InfoObject
    {
        [JsonProperty("isInternal")]
        public bool IsInternal { get; set; } = false;
        [JsonProperty("isHidden")]
        public bool IsHidden { get; set; } = true;
        [JsonProperty("value")]
        public string Value { get; set; } = string.Empty;
    }

    public class Profile
    {
        [JsonProperty("education")]
        public ProfileValue Education { get; set; } = new();
        [JsonProperty("experience")]
        public ProfileValue Experience { get; set; } = new();
        [JsonProperty("resume")]
        public ProfileValue Resume { get; set; } = new();
    }

    public class ProfileValue
    {
        [JsonProperty("isMandatory")]
        public bool IsMandatory { get; set; } = false;
        [JsonProperty("isHidden")]
        public bool IsHidden { get; set; } = true;
        [JsonProperty("value")]
        public string Value { get; set; } = string.Empty;
    }
}
