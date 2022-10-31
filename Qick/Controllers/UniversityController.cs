using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Responses;
using Qick.Repositories.Interfaces;

namespace Qick.Controllers
{
    [Authorize]
    [Route("api/university")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _repo;
        private readonly IMapper _mapper;

        public UniversityController(IUniversityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //Get all University
        [AllowAnonymous]
        [HttpGet("get-university")]
        public async Task<IActionResult> GetUniversity(string? status, Guid? UniId)
        {
            try
            {
                if(UniId != null)
                {
                    var response = await _repo.GetUniversityDetail(UniId);
                    return Ok(response);
                }
                else
                {
                    var response = await _repo.GetListAllUniversity(status);
                    var ListUniResponse = _mapper.Map<IEnumerable<ListUniResponse>>(response);
                    return Ok(ListUniResponse);
                }
                
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Get all University
        [AllowAnonymous]
        [HttpGet("get-university-spec")]
        public async Task<IActionResult> GetUniversitySpec(Guid? UniId)
        {
            try
            {
                    var response = await _repo.GetListAllUniversitySpec(UniId);
                    return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Get University by Major Id
        [AllowAnonymous]
        [HttpGet("get-uni-major")]
        public async Task<IActionResult> GetUniversityByMajorId(Guid majorId)
        {
            try
            {
                var response = await _repo.GetUniversityByMajorId(majorId);
                var ListUniResponse = _mapper.Map<IEnumerable<ListUniResponse>>(response);
                return Ok(ListUniResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
