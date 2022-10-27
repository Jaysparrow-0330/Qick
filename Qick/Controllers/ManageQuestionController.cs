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
    [Authorize(Roles = Roles.GOD + "," + Roles.ADMIN)]
    [Route("api/admin-question")]
    [ApiController]
    public class ManageQuestionController : ControllerBase
    {
        private readonly ITestRepository _repoTest;
        private readonly IQuestionRepository _repoQuestion;
        private readonly IOptionRepository _repoOption;
        private readonly IMapper _mapper;

        public ManageQuestionController(ITestRepository repoTest, IMapper mapper, IQuestionRepository repoQuestion, IOptionRepository repoOption)
        {
            _repoTest = repoTest;
            _mapper = mapper;
            _repoQuestion = repoQuestion;
            _repoOption = repoOption;
        }

        // Get list all question by test id
        [HttpGet("get-all-question")]
        public async Task<IActionResult> GetAllQuestionByTestId(int testId)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string Role = User.FindFirst(ClaimTypes.Role).Value.ToString();
                var questionList = await _repoQuestion.GetListQuestionBasedOnTestId(testId);
                var questionListResponse = _mapper.Map<IEnumerable<QuestionForAdminResponse>>(questionList);
                return Ok(questionListResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

       
    }
}
