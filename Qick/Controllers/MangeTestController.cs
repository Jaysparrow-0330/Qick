using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Repositories.Interfaces;
using Qick.Services.Interfaces;
using System.Security.Claims;

namespace Qick.Controllers
{
    [Authorize(Roles = Roles.GOD + "," + Roles.ADMIN)]
    [Route("api/managetest")]
    [ApiController]
    public class MangeTestController : ControllerBase
    {
        private readonly ITestRepository _repoTest;
        private readonly ICreateTokenService _token;
        private readonly IQuestionRepository _repoQuestion;
        private readonly IOptionRepository _repoOption;
        private readonly IMapper _mapper;

        public MangeTestController(ITestRepository repo, ICreateTokenService token, IMapper mapper, IQuestionRepository repoQuestion, IOptionRepository repoOption)
        {
            _repoTest = repo;
            _token = token;
            _mapper = mapper;
            _repoQuestion = repoQuestion;
            _repoOption = repoOption;
        }
        //Create Test step one create basic information of test , return test to create questions, option, etc.
        [HttpPost("create-test-step-one")]
        public async Task<IActionResult> CreateTestStepOne(CreateTestRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repoTest.CreateTest(request, userId);
                var testResponse = _mapper.Map<CreateTestResponseStepOne>(response);
                return Ok(testResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Create Test step two create questions and options , return boolean to check 
        [HttpPost("create-test-step-two")]
        public async Task<IActionResult> CreateTestStepTwo(CreateTestStepTwoRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = false;
                foreach (var ques in request.Questions)
                {
                    var question = await _repoQuestion.CreateQuestion(ques);
                    
                    foreach (var opt in ques.Options)
                    {
                        var check = await _repoOption.CreateOption(question, opt);
                        response = check;
                    }
                }
                
                if (response)
                {
                    return Ok(new HttpStatusCodeResponse(200));
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
        // Get list active test by admin
        [HttpGet("get-list-active-test-by-admin")]
        public async Task<IActionResult> GetAllActiveTestByAdmin()
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string Role = User.FindFirst(ClaimTypes.Role).Value.ToString();
                var testList = await _repoTest.GetListActiveTest(userId);
                var testListResponse = _mapper.Map<IEnumerable<ListTestResponse>>(testList);
                return Ok(testListResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        // Get list all test by admin
        [HttpGet("get-list-all-test-by-admin")]
        public async Task<IActionResult> GetAllTestByAdmin()
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string Role = User.FindFirst(ClaimTypes.Role).Value.ToString();
                var testList = await _repoTest.GetListAllTest(userId);
                var testListResponse = _mapper.Map<IEnumerable<ListTestResponse>>(testList);
                return Ok(testListResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        // Get question type category
        [HttpGet("get-active-question-type")]
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
    }
}
