﻿using Microsoft.AspNetCore.Mvc;
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

        // constructor
        public AuthController(IUserRepository repo, ICreateTokenService token )
        {
            _repo = repo;
            _token = token;
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
            catch (Exception ex)
            {
                return Ok(new HttpStatusCodeResponse(400,ex.Message));
            }
        }


        // Login admin page With Email And Password
        [HttpPost("login-admin")]
        public async Task<ActionResult> LoginAd(LoginRequest userIn)
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
            catch (Exception ex)
            {
                return Ok(new HttpStatusCodeResponse(400, ex.Message));
            }
        }

        // Login University page With Email And Password
        [HttpPost("login-university")]
        public async Task<ActionResult> LoginUni(LoginRequest userIn)
        {
            try
            {
                var check = await _repo.LoginUni(userIn);
                if (check == null) return Unauthorized();
                else
                {
                    string tk = _token.CreateToken(check);
                    return Ok(tk);
                }
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
            catch (Exception ex) { return BadRequest(ex); }
        }

    }
}