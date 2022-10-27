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

        // Get list test by guest
        [HttpGet("guest-get-list-active-test-guest")]
        public async Task<IActionResult> GetAllActiveTestByGuest()
        {
            try
            {
                var testList = await _repo.GetListActiveTestGuest();
                var testListResponse = _mapper.Map<IEnumerable<ListTestResponse>>(testList);
                return Ok(testListResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        // Get test detail by guest
        [HttpGet("guest-get-test-detail-guest")]
        public async Task<IActionResult> GetTestDetail(int testId)
        {
            try
            { 
                var testDetail = await _repo.GetTestById(testId);
                var testDetailResponse = _mapper.Map<TestDetailResponse>(testDetail);
                return Ok(testDetailResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        // Get test to attemp by guest
        [HttpGet("guest-taking-test-guest")]
        public async Task<IActionResult> TakingTest(int testId)
        {
            try
            {
                var takingTest = await _repo.GetTestToAttempForGuest(testId);
                var takingTestResponse = _mapper.Map<TakingTestResponse>(takingTest);
                return Ok(takingTestResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

        }

        // submit test response test's result
        [HttpPost("guest-submit-test")]
        public async Task<IActionResult> SubmitTest(CalculateResultRequest request)
        {
            try
            {
                var result = await _repo.CalculateTestResult(request);
                var submitResponse = _mapper.Map<SubmitResponse>(result);
                return Ok(submitResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

        }
    }
}
