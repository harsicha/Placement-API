using CapitalPlacement.DatabaseModels;
using CapitalPlacement.DataTransferModels;

namespace CapitalPlacement.Abstracts
{
    public interface IProgramService
    {
        Task<ProgramDTO?> GetAsync(string id);
        Task<string?> CreateAsync(ProgramDTO application);
        Task<dynamic> UpdateAsync(ProgramDTO program);
    }
}
