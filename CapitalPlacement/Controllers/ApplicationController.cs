using CapitalPlacement.Abstracts;
using CapitalPlacement.DataTransferModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CapitalPlacement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _service;

        public ApplicationController(IApplicationService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all details related to the Application Tab. ApplicationId is required.
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(string appId)
        {
            try
            {
                var response = await _service.GetAsync(appId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in Get method of ProgramController: " + ex.ToString());
                return BadRequest("Please try again later");
            }
        }

        /// <summary>
        /// Post and save all the details related to the Application tab. ApplicationId is required.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] ApplicationDTO application)
        {
            try
            {
                var response = await _service.UpdateAsync(application);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in Put method of ProgramController: " + ex.ToString());
                return BadRequest("Please try again later");
            }
        }
    }
}
