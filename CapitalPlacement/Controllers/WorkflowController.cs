using CapitalPlacement.Abstracts;
using CapitalPlacement.DatabaseModels;
using CapitalPlacement.DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CapitalPlacement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowService _service;

        public WorkflowController(IWorkflowService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get a list of all stages related to the applicationId.
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<dynamic> Get(string appId)
        {
            try
            {
                var response = await _service.GetAsync(appId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in Get method of WorkflowController: " + ex.ToString());
                return BadRequest("Please try again later");
            }
        }

        /// <summary>
        /// Post and save the stages. ApplicationId is required.
        /// </summary>
        /// <param name="stages"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] List<Stage> stages)
        {
            try
            {
                var response = await _service.UploadAsync(stages);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in Put method of WorkflowController: " + ex.ToString());
                return BadRequest("Please try again later");
            }
        }
    }
}
