using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Repositories.Interfaces;

namespace Qick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly ITestRepository _repo;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="repo"></param>
        /// 
        public GuestController(ITestRepository repo)
        {
            _repo = repo;
        }

        // POST api/<ValuesController>
        /// <summary>
        /// Get list test by guest
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-list-test-guest")]
        public async Task<IActionResult> GetCampaigns()
        {
            try
            {
                

                var test = await _repo.GetListTestGuest();
                return Ok(test);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }



        }
    }
}
