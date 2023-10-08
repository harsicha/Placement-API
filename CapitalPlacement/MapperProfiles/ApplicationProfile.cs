using CapitalPlacement.DatabaseModels;
using CapitalPlacement.DataTransferModels;
using Profile = AutoMapper.Profile;

namespace CapitalPlacement.MapperProfiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ProgramDTO, Application>();
        }
    }
}
