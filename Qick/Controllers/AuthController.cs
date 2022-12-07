using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Enum;
using Qick.Dto.Exceptions;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Repositories.Interfaces;
using Qick.Services.Interfaces;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Qick.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly ICreateTokenService _token;
        private readonly IGenerateRandomService _random;
        private readonly ISendMailService _mail;

        // constructor
        public AuthController(ISendMailService mail,IUserRepository repo, ICreateTokenService token, IGenerateRandomService random)
        {
            _repo = repo;
            _token = token;
            _random = random;
            _mail = mail;
        }


        // Login With Email And Password
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
            catch (NotActiveException ex)
            {
                return Ok(new HttpStatusCodeResponse(210));
            }
            catch (Exception ex)
            {
                return Ok(new HttpStatusCodeResponse(400,ex.Message));
            }
        }


        // Login admin page With Email And Password
        [HttpPost("login-admin-university")]
        public async Task<ActionResult> LoginAdUni(LoginRequest userIn)
        {
            try
            {
                var check = await _repo.LoginAd(userIn);
                if (check == null) return Unauthorized();
                else
                {
                    string tk = _token.CreateToken(check);
                    return Ok(tk);
                }
            }
            catch (NotActiveException ex)
            {
                return Ok(new HttpStatusCodeResponse(210));
            }
            catch (Exception ex)
            {
                return Ok(new HttpStatusCodeResponse(400, ex.Message));
            }
        }

        // Login admin page With Email And Password
        [HttpPost("active")]
        public async Task<ActionResult> ActiveUser(ActiveRequest request)
        {
            try
            {
                var user = await _repo.GetUserByEmail(request.Email);
                if (user != null)
                {
                    var check = await _repo.ActiveUserStatus(user, request.Code);
                    if (check == null) return null;
                }
                return Ok(new LoginResponse { Token = _token.CreateToken(user) });
            }
            catch (NotActiveException ex)
            {
                return Ok(new HttpStatusCodeResponse(210));
            }
            catch (Exception ex)
            {
                return Ok(new HttpStatusCodeResponse(400, ex.Message));
            }
        }
        /// <summary>
        /// Register to a member role
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                var check = await _repo.EmailExist(request.Email);
            if (check)
            {
                return Ok(new HttpStatusCodeResponse(410));
            }
                string code = _random.GenerateRandomNumber(6);
                var user = await _repo.Register(request, code);

            if (!(user == null))
            {
                    await _mail.SendMailAsync(request.Email, code, "Active Code", "HTMLTemplates/active-code-email.html");
                    return Ok(new HttpStatusCodeResponse(200));
            }
            else
            {
                throw new Exception("Error");
            }
            }
            catch (Exception ex) { return BadRequest(ex); }
        }

        /// <summary>
        /// Register to a member role
        [Authorize(Roles = Roles.ADMIN)]
        [HttpPost("register-manager")]
        public async Task<IActionResult> RegisterManager(ManagerStaffRequest request)
        {
            try
            {
                var check = await _repo.EmailExistUniMa(request.Email);
                if (check)
                {
                    return Ok(new HttpStatusCodeResponse(410));
                }
                string code = _random.GenerateRandomString(12);
                var user = await _repo.RegisterUni(request, code);

                if (!(user == null))
                {
                    await _mail.SendMailAsync(request.Email, code, "Access Allowed", "HTMLTemplates/create-uni-email.html");
                    return Ok(new HttpStatusCodeResponse(200));
                }
                else
                {
                    throw new Exception("Error");
                }
            }
            catch (Exception ex) { return BadRequest(ex); }
        }
        /// <summary>
        /// Register to a member role
        [Authorize(Roles = Roles.MANAGER)]
        [HttpPost("register-staff")]
        public async Task<IActionResult> RegisterStaff(ListStaffRequest request)
        {
            try
            {
                foreach (var staff in request.staffs)
                {
                    var check = await _repo.EmailExistStaff(staff.Email);
                    if (check)
                    {
                        return Ok(new HttpStatusCodeResponse(410));
                    }
                    string code = _random.GenerateRandomString(12);
                    var user = await _repo.RegisterStaff(staff, code);

                    if (user == null)
                    {
                        return Ok(new HttpStatusCodeResponse(400));
                    } 
                    else
                    {
                        await _mail.SendMailAsync(staff.Email, code, "Access Allowed", "HTMLTemplates/create-uni-email.html");
                    }
                }
                    return Ok(new HttpStatusCodeResponse(200));
                
            }
            catch (Exception ex) { return BadRequest(ex); }
        }
    }
}
