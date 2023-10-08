using AutoMapper;
using CapitalPlacement.Abstracts;
using CapitalPlacement.DatabaseModels;
using CapitalPlacement.DataLayer;
using System.Diagnostics;

namespace CapitalPlacement.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly DBContext _dbContext;

        public WorkflowService(DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        /// <summary>
        /// Get all stages related to the passed applicationId.
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<dynamic> GetAsync(string appId)
        {
            try
            {
                var response = await _dbContext.GetStagesByAppId(appId);
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in GetAsync method of WorkflowService: " + ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Save all the stages in the DB.
        /// </summary>
        /// <param name="stages"></param>
        /// <returns></returns>
        public async Task<dynamic> UploadAsync(List<Stage> stages)
        {
            try
            {
                foreach (var stage in stages)
                {
                    if (!string.IsNullOrEmpty(stage.Id))
                    {
                        await _dbContext.UpdateStage(stage);
                        continue;
                    }
                    var id = await _dbContext.CreateStage(stage);
                    if (string.IsNullOrEmpty(id))
                    {
                        return "Something went wrong";
                    }
                    stage.Id = id;
                }
                return stages;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in UploadAsync method of WorkflowService: " + ex.ToString());
                throw;
            }
        }
    }
}
