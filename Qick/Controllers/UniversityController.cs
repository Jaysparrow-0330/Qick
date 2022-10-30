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
        public async Task<IActionResult> GetUniversity(string status)
        {
            try
            {
                var response = await _repo.GetListAllUniversity(status);
                var ListJobResponse = _mapper.Map<IEnumerable<ListUniResponse>>(response);
                return Ok(ListJobResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

    }
}
