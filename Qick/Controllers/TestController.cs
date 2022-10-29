using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;
using Qick.Repositories.Interfaces;
using Qick.Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Qick.Controllers
{
    [Authorize]
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestRepository _repo;
        private readonly ICreateTokenService _token;
        private readonly IQuestionRepository _repoQuestion;
        private readonly IOptionRepository _repoOption;
        private readonly IMapper _mapper;

        // constructor
        public TestController(ITestRepository repo, ICreateTokenService token, IMapper mapper,IQuestionRepository repoQuestion,IOptionRepository repoOption)
        {
            _repo = repo;
            _token = token;
            _mapper = mapper;
            _repoQuestion = repoQuestion;
            _repoOption = repoOption;
        }

        // Get list active test
        [AllowAnonymous]
        [HttpGet("get-active-test")]
        public async Task<IActionResult> GetAllActiveTest()
        {
            try
            {
                var testList = await _repo.GetListActiveTest();
                var testListResponse = _mapper.Map<IEnumerable<ListTestResponse>>(testList);
                return Ok(testListResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        // Get test to attemp by user
        [AllowAnonymous]
        [HttpGet("taking-test")]
        public async Task<IActionResult> TakingTest(int testId) 
        {
            try
            {
                var takingTest = await _repo.GetTestToAttemp(testId);
                var takingTestResponse = _mapper.Map<TakingTestResponse>(takingTest);
                return Ok(takingTestResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        
        }

        // Get test category
        [HttpGet("get-test-type")]
        public async Task<IActionResult> GetActiveTestType()
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var testTypes = await _repo.GetActiveTestType();
                var testTypeResponse = _mapper.Map<IEnumerable<TestTypeResponse>>(testTypes);
                return Ok(testTypeResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

        }

        // Get test detail
        [AllowAnonymous]
        [HttpGet("get-test-detail")]
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

        // submit test response test's result by guest
        [AllowAnonymous]
        [HttpPost("submit-test-guest")]
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
