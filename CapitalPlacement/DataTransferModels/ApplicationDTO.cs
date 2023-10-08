using CapitalPlacement.DatabaseModels;
using Newtonsoft.Json;
using File = CapitalPlacement.DatabaseModels.File;

namespace CapitalPlacement.DataTransferModels
{
    public class ApplicationDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("file")]
        public IFormFile? File { get; set; }
        [JsonProperty("personalInformation")]
        public PersonalInformationDTO PersonalInformation { get; set; } = new();
        [JsonProperty("profile")]
        public ProfileDTO Profile { get; set; } = new();
        [JsonProperty("coverImageId")]
        public string CoverImageId { get; set; } = string.Empty;
        [JsonProperty("additionalQuestions")]
        public List<AdditionalQuestionDTO> AdditionalQuestions { get; set; } = new();
        [JsonProperty("fileResponse")]
        public File? FileResponse { get; set; }
    }

    public class ProfileDTO : Profile
    {
        [JsonProperty("experiences")]
        public List<Experience> Experiences { get; set; } = new();
        [JsonProperty("additionalQuestions")]
        public List<AdditionalQuestionDTO> AdditionalQuestions { get; set; } = new();
    }

    public class PersonalInformationDTO : PersonalInformation
    {
        [JsonProperty("additionalQuestions")]
        public List<AdditionalQuestionDTO> AdditionalQuestions { get; set; } = new();
    }

    public class AdditionalQuestionDTO : AdditionalQuestion
    {
        [JsonProperty("file")]
        public IFormFile? File { get; set; }
        [JsonProperty("fileResponse")]
        public File? FileResponse { get; set; }
    }
}
