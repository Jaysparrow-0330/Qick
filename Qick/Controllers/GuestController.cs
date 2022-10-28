using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Repositories.Interfaces;

namespace Qick.Controllers
{
    [Route("api/guest")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly ITestRepository _repo;
        private readonly IMapper _mapper;
        // constructor
        public GuestController(ITestRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // submit test response test's result
        [HttpPost("submit-test")]
        public async Task<IActionResult> SubmitTest(CalculateResultRequest request)
        {
            try
            {
                var result = await _repo.CalculateTestResult(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

        }
    }
}
