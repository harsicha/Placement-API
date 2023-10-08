using CapitalPlacement.DataTransferModels;

namespace CapitalPlacement.Abstracts
{
    public interface IApplicationService
    {
        Task<dynamic> UpdateAsync(ApplicationDTO application);
        Task<dynamic> GetAsync(string appId);
    }
}
