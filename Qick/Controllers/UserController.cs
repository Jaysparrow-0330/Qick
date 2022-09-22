using Microsoft.AspNetCore.Mvc;
using Qick.Model.Input;
using Qick.Model.Output;
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
        public async Task<IActionResult> Login(LoginInput userIn)
        {
            LoginOutput user = await _repo.Login(userIn);

            if (user == null)
                return Unauthorized();

            LoginOutput login = new()
            {
                
                UserName = user.UserName,
                Email = user.Email,
                
            };
            return Ok(login);
        }

    }
}
