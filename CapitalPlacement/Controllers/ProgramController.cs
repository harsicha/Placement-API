using CapitalPlacement.Abstracts;
using CapitalPlacement.DatabaseModels;
using CapitalPlacement.DataTransferModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CapitalPlacement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _service;

        public ProgramController(IProgramService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all data related to the Program Tab
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
        /// Post and save the Program related data. This endpoint returns the applicationId which must be used across all the tabs to further process information
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProgramDTO program)
        {
            try
            {
                var response = await _service.CreateAsync(program);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in Post method of ProgramController: " + ex.ToString());
                return BadRequest("Please try again later");
            }
        }

        /// <summary>
        /// Update the program details. ApplicationId is required.
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProgramDTO program)
        {
            try
            {
                var response = await _service.UpdateAsync(program);
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
