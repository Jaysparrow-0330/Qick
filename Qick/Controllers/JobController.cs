using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Responses;
using Qick.Repositories.Interfaces;
using System.Security.Claims;

namespace Qick.Controllers
{
    [Authorize]
    [Route("api/job")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository _repo;
        private readonly IMapper _mapper;
        public JobController(IJobRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //Get all Job
        [AllowAnonymous]
        [HttpGet("get-job")]
        public async Task<IActionResult> GetJob()
        {
            try
            {
                var response = await _repo.GetAllJob();
                var ListJobResponse = _mapper.Map<IEnumerable<JobResponse>>(response);
                return Ok(ListJobResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Get all Job
        [AllowAnonymous]
        [HttpGet("get-job-career")]
        public async Task<IActionResult> GetJobByCharacterId(Guid CharacterId)
        {
            try
            {
                var response = await _repo.GetJobByCharacterId(CharacterId);
                var ListJobResponse = _mapper.Map<IEnumerable<CareerJobResponse>>(response);

                return Ok(ListJobResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
