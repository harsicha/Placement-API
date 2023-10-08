using AutoMapper;
using CapitalPlacement.Abstracts;
using CapitalPlacement.DatabaseModels;
using CapitalPlacement.DataLayer;
using CapitalPlacement.DataTransferModels;

namespace CapitalPlacement.Services
{
    public class ProgramService : IProgramService
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;

        public ProgramService(DBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the existing application record from DB and map to DTO object to return relevant data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProgramDTO?> GetAsync(string id)
        {
            var application = await _dbContext.GetApplication(id);
            var program = _mapper.Map<ProgramDTO>(application);
            return program;
        }

        /// <summary>
        /// Create a record in DB with the program tab data.
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        public async Task<string?> CreateAsync(ProgramDTO program)
        {
            var application = _mapper.Map<Application>(program);
            return await _dbContext.CreateApplication(application);
        }

        /// <summary>
        /// Update the existing program tab data in DB.
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        public async Task<dynamic> UpdateAsync(ProgramDTO program)
        {
            var existingApp = await _dbContext.GetApplication(program.Id);
            if (existingApp == null)
            {
                return "Please provide a valid id";
            }
            var application = _mapper.Map<ProgramDTO, Application>(program, existingApp);
            return await _dbContext.UpdateApplication(application);
        }
    }
}
