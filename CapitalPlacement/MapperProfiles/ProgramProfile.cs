using Profile = AutoMapper.Profile;
using CapitalPlacement.DatabaseModels;
using CapitalPlacement.DataTransferModels;

namespace CapitalPlacement.MapperProfiles
{
    public class ProgramProfile : Profile
    {
        public ProgramProfile()
        {
            CreateMap<Application, ProgramDTO>();
        }
    }
}
