using AutoMapper;
using CapitalPlacement.DataTransferModels;
using Profile = AutoMapper.Profile;

namespace CapitalPlacement.MapperProfiles
{
    public class ProfileProfile : Profile
    {
        public ProfileProfile()
        {
            CreateMap<ProfileDTO, DatabaseModels.Profile>();
            CreateMap<DatabaseModels.Profile, ProfileDTO>();
        }
    }
}
