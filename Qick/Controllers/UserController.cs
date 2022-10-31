using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Repositories.Interfaces;
using System.Security.Claims;

namespace Qick.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //Update User Profiel
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UserProfileUpdateRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (userId != null)
                {
                    var user = await _repo.UpdateProfile(request);
                    return Ok(user);
                }
                else
                {
                    return Ok(new HttpStatusCodeResponse(204));
                }
                
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Update ACc Profiel
        [HttpPut("update-aca")]
        public async Task<IActionResult> UpdateUserAcedemicProfile(UpdateAcademyRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (userId != null)
                {
                    var user = await _repo.UpdateAcademicProfile(request);
                    return Ok(user);
                }
                else
                {
                    return Ok(new HttpStatusCodeResponse(204));
                }
                
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Get profile
        [HttpGet("get-profile")]
        public async Task<IActionResult> GetProfile(Guid userId)
        {
            try
            {
                var response = await _repo.GetProfile(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Get academy profile
        [HttpGet("get-aca-profile")]
        public async Task<IActionResult> GetAcademicProfile(Guid userId)
        {
            try
            {
                var response = await _repo.GetAcademicProfile(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Create Academic profile
        [HttpPost("create-aca-profile")]
        public async Task<IActionResult> CreateAcademicprofile(CreateAcademyRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var response = await _repo.CreateAcademicProfile(request);
                    if (response != null)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return Ok(new HttpStatusCodeResponse(204));
                    }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
