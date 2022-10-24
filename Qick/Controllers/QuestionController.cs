using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Responses;
using Qick.Repositories.Interfaces;
using Qick.Services.Interfaces;
using System.Security.Claims;

namespace Qick.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ITestRepository _repoTest;
        private readonly ICreateTokenService _token;
        private readonly IQuestionRepository _repoQuestion;
        private readonly IOptionRepository _repoOption;
        private readonly IMapper _mapper;

        public QuestionController(ITestRepository repoTest, ICreateTokenService token, IMapper mapper, IQuestionRepository repoQuestion, IOptionRepository repoOption)
        {
            _repoTest = repoTest;
            _token = token;
            _mapper = mapper;
            _repoQuestion = repoQuestion;
            _repoOption = repoOption;
        }

        // Get question type category
        [HttpGet("authenticated-user-get-active-question-type")]
        public async Task<IActionResult> GetActiveQuestionType()
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var quesTypes = await _repoQuestion.GetActiveQuestionType();
                var quesTypeResponse = _mapper.Map<IEnumerable<QuestionTypeResponse>>(quesTypes);
                return Ok(quesTypeResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

        }

        // Get question by test id
        [HttpGet("authenticated-user-get-question-by-test-id")]
        public async Task<IActionResult> GetTestDetail(int testId)
        {
            try
            {
                var testDetail = await _repoTest.GetTestById(testId);
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
