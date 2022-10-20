using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="repo"></param>
        /// 
        public TestController(ITestRepository repo, ICreateTokenService token, IMapper mapper,IQuestionRepository repoQuestion,IOptionRepository repoOption)
        {
            _repo = repo;
            _token = token;
            _mapper = mapper;
            _repoQuestion = repoQuestion;
            _repoOption = repoOption;
        }

        /// <summary>
        /// Get list test by authenticated user
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-list-test-by-user")]
        public async Task<IActionResult> GetAllTest()
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string Role = User.FindFirst(ClaimTypes.Role).Value.ToString();
                var testList = await _repo.GetListTest(userId);
                var testListResponse = _mapper.Map<ListTestResponse>(testList);
                return Ok(testListResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            
            

        }

        /// <summary>
        /// Get test to attemp by user
        /// </summary>
        /// <returns></returns>
        [HttpGet("taking-test")]
        public async Task<IActionResult> TakingTest(int testId) 
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string Role = User.FindFirst(ClaimTypes.Role).Value.ToString();
                var takingTest = await _repo.GetTestToAttempForUser(testId,userId);
                var takingTestResponse = _mapper.Map<TakingTestResponse>(takingTest);
                return Ok(takingTestResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        
        }

        /// <summary>
        /// Get test detail by user
        /// </summary>
        /// <returns></returns>
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


    }
}
