using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Enum;
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
        private readonly ITestRepository _repoTest;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repo, IMapper mapper, ITestRepository repoTest)
        {
            _repo = repo;
            _mapper = mapper;
            _repoTest = repoTest;
        }


        //Get profile
        [HttpGet("get-profile")]
        public async Task<IActionResult> GetProfile()
        {

            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repo.GetProfile(userId);
                var profile = _mapper.Map<ProfileResponse>(response);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Get profile
        [HttpGet("save-unis")]
        public async Task<IActionResult> GetListSaveUni()
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repo.GetAllUniSavedByUserId(userId);
                var profile = _mapper.Map<IEnumerable<SaveUniResponse>>(response);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        //Get profile
        [HttpGet("attempts")]
        public async Task<IActionResult> GetAtemptByUser()
        {

            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repoTest.GetAttempt(userId);
                var profile = _mapper.Map<IEnumerable<ListAttemptResponse>>(response);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        //Get profile
        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {

            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repo.GetAllUser();
                var profile = _mapper.Map<IEnumerable<ProfileResponse>>(response);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Get academy profile
        [HttpGet("get-aca-profile")]
        public async Task<IActionResult> GetAcademicProfile()
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
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
        public async Task<IActionResult> CreateAcademicprofile()
        {
            try
            {
                    Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var response = await _repo.CreateAcademicProfile(userId);
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

        //Create Academic profile
        [HttpPost("save-uni")]
        public async Task<IActionResult> SaveUni(SaveUniRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repo.SaveUni(request, userId);
                if (response)
                {
                    return Ok(new HttpStatusCodeResponse(200));
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

        //Create Academic profile
        [HttpPost("check-save")]
        public async Task<IActionResult> CheckSaveUni(SaveUniRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repo.CheckSaveUni(request, userId);
                if (response)
                {
                    return Ok(new HttpStatusCodeResponse(200));
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
        //Update User Profiel
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UserProfileUpdateRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (userId != null)
                {
                    var user = await _repo.UpdateProfile(request, userId);
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
        //Update User Profiel
        [HttpPut("update-pass")]
        public async Task<IActionResult> UpdatePass(UpdatePassRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var user = await _repo.GetUserById(userId);
                if (user != null)
                {
                    var update = await _repo.UpdatePassword(request, user);

                    if (update != null)
                    {
                        return Ok(new HttpStatusCodeResponse(200));
                    }
                    else
                    {
                        return Ok(new HttpStatusCodeResponse(204));
                    }
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

        //Update ACc Profiel
        [HttpPut("public")]
        public async Task<IActionResult> UpdatePublicProfile()
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (userId != null)
                {
                    var check = await _repo.PublicProfileUser(userId);
                    if (check)
                    {
                        return Ok(new HttpStatusCodeResponse(200));
                    }
                    else
                    {
                        return Ok(new HttpStatusCodeResponse(204));
                    }
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
