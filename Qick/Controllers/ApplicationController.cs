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
    [Route("api/application")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationRepository _repo;
        private readonly IMapper _mapper;

        public ApplicationController(IApplicationRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //Create Academic profile
        [HttpPost("create-application")]
        public async Task<IActionResult> CreateApplication(CreateApplicationRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repo.CreateApplication(request);
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
        [HttpPost("create-application-detail")]
        public async Task<IActionResult> CreateApplicationDetail(CreateApplicationDetailRequest AppDetail)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repo.CreateApplicationDetail(AppDetail);
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

        // Get list all question by test id
        [HttpGet("get-all-application")]
        public async Task<IActionResult> GetAllApplicationById(Guid? Id)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string Role = User.FindFirst(ClaimTypes.Role).Value.ToString();
                if(Role.Equals(Roles.MEMBER) || Role.Equals(Roles.STUDENT))
                {
                    var applicationList = await _repo.GetApplicationByUserId(Id);
                    var applicationListResponse = _mapper.Map<IEnumerable<ListApplicationResponse>>(applicationList);
                    return Ok(applicationListResponse);
                } 
                else if(Role.Equals(Roles.STAFF) || Role.Equals(Roles.MANAGER))
                {
                    var applicationList = await _repo.GetApplicationByUniId(Id);
                    var applicationListResponse = _mapper.Map<IEnumerable<ListApplicationResponse>>(applicationList);
                    return Ok(applicationListResponse);
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

        // Get list all question by test id
        [HttpGet("get-detail")]
        public async Task<IActionResult> GetApplicationDetail(Guid? Id)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string Role = User.FindFirst(ClaimTypes.Role).Value.ToString();
                    var applicationDetail = await _repo.GetApplicationDetail(Id);
                    var applicationResponse = _mapper.Map<ApplicationDetailResponse>(applicationDetail);
                    return Ok(applicationResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Update app status
        [HttpPut("update-app-status")]
        public async Task<IActionResult> UpdateApplicationStatus(string status, Guid? AppId)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var checkTest = await _repo.ChangeStatusApplication(status,AppId);
                if (checkTest != null)
                {
                    return Ok(checkTest);
                }
                else
                {
                    return Ok(new HttpStatusCodeResponse(310));
                }
                
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
