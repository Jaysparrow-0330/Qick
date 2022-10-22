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
        //Create Test step one , return test to create questions, option, etc.
        [HttpPost("create-test")]
        public async Task<IActionResult> CreateTest(CreateTestRequest request)
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
    }
}
