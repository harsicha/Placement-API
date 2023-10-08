using CapitalPlacement.DatabaseModels;
using CapitalPlacement.DataTransferModels;
using Profile = AutoMapper.Profile;

namespace CapitalPlacement.MapperProfiles
{
    public class AdditionalQuestionProfile : Profile
    {
        public AdditionalQuestionProfile()
        {
            CreateMap<AdditionalQuestionDTO, AdditionalQuestion>();
            CreateMap<AdditionalQuestion, AdditionalQuestionDTO>();
        }
    }
}
