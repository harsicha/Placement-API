using CapitalPlacement.DatabaseModels;

namespace CapitalPlacement.Abstracts
{
    public interface IWorkflowService
    {
        Task<dynamic> GetAsync(string appId);
        Task<dynamic> UploadAsync(List<Stage> stages);
    }
}
