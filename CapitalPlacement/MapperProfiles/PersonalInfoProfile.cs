using AutoMapper;
using CapitalPlacement.DatabaseModels;
using CapitalPlacement.DataTransferModels;
using Profile = AutoMapper.Profile;

namespace CapitalPlacement.MapperProfiles
{
    public class PersonalInfoProfile : Profile
    {
        public PersonalInfoProfile()
        {
            CreateMap<PersonalInformationDTO, PersonalInformation>();
            CreateMap<PersonalInformation, PersonalInformationDTO>();
        }
    }
}
