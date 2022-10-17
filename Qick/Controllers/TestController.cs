using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qick.Repositories.Interfaces;
using Qick.Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Qick.Controllers
{
    [Authorize]
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestRepository _repo;
        private readonly ICreateTokenService _token;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="repo"></param>
        /// 
        public TestController(ITestRepository repo, ICreateTokenService token)
        {
            _repo = repo;
            _token = token;
        }
        // POST api/<ValuesController>
        /// <summary>
        /// Get list test by authenticated user
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-list-test")]
        public async Task<IActionResult> GetCampaigns()
        {
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
                string Role = User.FindFirst(ClaimTypes.Role).Value.ToString();

                var test = await _repo.GetListTest(userId);
                return Ok(test);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            
            

        }

       
    }
}
