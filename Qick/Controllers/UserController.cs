using Microsoft.AspNetCore.Mvc;
using Qick.Controllers.Requests;
using Qick.Controllers.Responses;
using Qick.Repositories.Interfaces;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Qick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
      

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="repo"></param>
        /// 
        public UserController(IUserRepository repo)
        {
            _repo = repo;
           
        }


        // POST api/<ValuesController>
        /// <summary>
        /// Login With Email And Password
        /// </summary>
        /// <param name="userIn"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest userIn)
        {
            var user = await _repo.Login(userIn);

            if (user == null)
                return Unauthorized();

            LoginResponse login = new()
            {
                
               
            };
            return Ok(login);
        }

        /// <summary>
        /// Register to a member role
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            
            var user = await _repo.Register(request);

            if (!(user == null))
            {
                return Ok(new HttpStatusCodeResponse(200));
            }
            else
            {
                throw new Exception("Error");
            }
        }

    }
}
