using Microsoft.AspNetCore.Mvc;
using Qick.Controllers.Requests;
using Qick.Controllers.Responses;
using Qick.Handler.HandlerInterfaces;
using Qick.Handler.LoginHandler;
using Qick.Repositories.Interfaces;
using Qick.Services.Interfaces;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Qick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly ICreateTokenService _token;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="repo"></param>
        /// 
        public UserController(IUserRepository repo, ICreateTokenService token )
        {
            _repo = repo;
            _token = token;
        }


        // POST api/<ValuesController>
        /// <summary>
        /// Login With Email And Password
        /// </summary>
        /// <param name="userIn"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest userIn)
        {
            try
            {
                var check = await _repo.Login(userIn);
                if (check == null) return Unauthorized();
                else
                {
                    string tk = _token.CreateToken(check);
                    return Ok(tk);
                }
            }
            catch (Exception ex)
            {
                return Ok(new HttpStatusCodeResponse(400,ex.Message));
            }
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
