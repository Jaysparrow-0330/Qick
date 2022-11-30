﻿using AutoMapper;
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
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ITestRepository _repoTest;
        private readonly IQuestionRepository _repoQuestion;
        private readonly IOptionRepository _repoOption;
        private readonly ISystemRepository _repoSystem;
        private readonly IUniversityRepository _repoUni;
        private readonly IJobRepository _repoJob;
        private readonly IUserRepository _repoUser;
        private readonly IApplicationRepository _repoApp;
        private readonly IMapper _mapper;

        public AdminController(IApplicationRepository repoApp,IUserRepository repoUser,IJobRepository repoJob,IUniversityRepository repoUni,ISystemRepository repoSystem,ITestRepository repoTest, IMapper mapper, IQuestionRepository repoQuestion, IOptionRepository repoOption)
        {
            _repoTest = repoTest;
            _mapper = mapper;
            _repoQuestion = repoQuestion;
            _repoOption = repoOption;
            _repoSystem = repoSystem;
            _repoUni = repoUni;
            _repoJob = repoJob;
            _repoUser = repoUser;
            _repoApp = repoApp;
        }

        // Get list all question by test id
        [HttpGet("get-all-question")]
        public async Task<IActionResult> GetAllQuestionByAdmin(int testId)
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

        // Get list all test by admin
        [HttpGet("get-all-test")]
        public async Task<IActionResult> GetAllTestByAdmin()
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string Role = User.FindFirst(ClaimTypes.Role).Value.ToString();
                var testList = await _repoTest.GetListAllTest(userId);
                var testListResponse = _mapper.Map<IEnumerable<ListTestForAdminResponse>>(testList);
                return Ok(testListResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Get profile
        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {

            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repoUser.GetUser();
                var profile = _mapper.Map<IEnumerable<ListUserResponse>>(response);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        // Get question type category
        [HttpGet("get-question-type")]
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

        //Get all Job
        [HttpGet("get-job")]
        public async Task<IActionResult> GetJob()
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repoJob.GetAllJob();
                var ListJobResponse = _mapper.Map<IEnumerable<JobResponse>>(response);

                return Ok(ListJobResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        //Create Test step one create basic information of test , return test to create questions, option, etc.
        [HttpPost("test-create")]
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

        //Create Test step one create basic information of test , return test to create questions, option, etc.
        [HttpPost("jobmapping-create")]
        public async Task<IActionResult> CreateJobMapping(JobCharMappingRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repoSystem.CreateJobCharMapping(request);
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

        //Create Test step one create basic information of test , return test to create questions, option, etc.
        [HttpPost("majormapping-create")]
        public async Task<IActionResult> CreateMajorMapping(JobMajorMappingRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repoSystem.CreateJobMajorMapping(request);
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


        //Create Result for test
        [HttpPost("create-result")]
        public async Task<IActionResult> CreateTestResult(ResultRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repoTest.CreateResult(request);

                if (response )
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

        //Create University
        [HttpPost("create-university")]
        public async Task<IActionResult> CreateUniversity(CreateUniversityRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repoUni.CreateUniversity(request);

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
        //Create University
        [HttpPost("create-highschool")]
        public async Task<IActionResult> CreateHighSchool (CreateHighSchoolRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repoApp.CreateHighSchool(request);

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
        //Create Specialization
        [HttpPost("create-specialization")]
        public async Task<IActionResult> CreateSpec(SpecRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repoSystem.CreateSpec(request);

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

        //Create Test step two create questions and options , return boolean to check 
        [HttpPost("question-create")]
        public async Task<IActionResult> CreateTestStepTwo(CreateTestStepTwoRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var testUpdate = _repoTest.UpdateTotalQuestion(request.TotalQuestion, request.Questions.FirstOrDefault().TestId);
                foreach (var question in request.Questions)
                {
                    var checkQuestion = await _repoQuestion.GetQuestionById(question.QuestionId);
                    if (checkQuestion != null)
                    {
                        var updateQuestion = await _repoQuestion.UpdateQuestionInformation(question);
                        foreach (var option in question.Options)
                        {
                            var checkOption = await _repoOption.UpdateOptionInformation(option);
                            if (checkOption != null)
                            {
                                var updateOption = await _repoOption.UpdateOptionInformation(option);
                            }
                            else
                            {
                                var newOption = _mapper.Map<OptionRequest>(option);
                                var check = await _repoOption.CreateOption(updateQuestion, newOption);
                                if (!check)
                                {
                                    return Ok(new HttpStatusCodeResponse(204));
                                }
                            }
                        }
                    }
                    else
                    {
                        var newQuestion = _mapper.Map<QuestionRequest>(question);
                        var addQuestion = await _repoQuestion.CreateQuestion(newQuestion);
                        foreach (var opt in newQuestion.Options)
                        {
                            var check = await _repoOption.CreateOption(addQuestion, opt);
                            if (!check)
                            {
                                return Ok(new HttpStatusCodeResponse(204));
                            }
                        }
                    }
                }
                return Ok(new HttpStatusCodeResponse(200));
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Create Job
        [HttpPost("create-job")]
        public async Task<IActionResult> CreateJob(JobRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repoSystem.CreateJob(request);

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

        //Create Major
        [HttpPost("create-major")]
        public async Task<IActionResult> CreateMajor(MajorRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repoSystem.CreateMajor(request);

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

        //Update Test
        [HttpPut("update-test")]
        public async Task<IActionResult> UpdateTestByAdmin(UpdateTestRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var checkTest = await _repoTest.GetTestById(request.Id);
                if (checkTest != null)
                {
                    var test = await _repoTest.UpdateTestInformation(request);
                }
                else
                {
                    return Ok(new HttpStatusCodeResponse(310));
                }
                return Ok(new HttpStatusCodeResponse(200));
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Update ACc Profiel
        [HttpPut("ban/unban")]
        public async Task<IActionResult> BanUnbanUser(Guid userId)
        {
            try
            {
                if (userId != null)
                {
                    var check = await _repoUser.BanUser(userId);
                    if (check != null)
                    {
                        return Ok(check);
                    }
                    else
                    {
                        return Ok(new HttpStatusCodeResponse(204));
                    }
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
        //Update ACc Profiel
        [HttpPut("role")]
        public async Task<IActionResult> ChangeRoleUser(Guid userId, string roleId)
        {
            try
            {
                if (userId != null)
                {
                    var check = await _repoUser.RoleUser(userId, roleId);
                    if (check != null)
                    {
                        return Ok(check);
                    }
                    else
                    {
                        return Ok(new HttpStatusCodeResponse(204));
                    }
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
        //Update List Question
        //[HttpPut("update-questions")]
        //public async Task<IActionResult> UpdateQuestionByAdmin(CreateTestStepTwoRequest request)
        //{
        //    try
        //    {
        //        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //        var testUpdate = _repoTest.UpdateTotalQuestion(request.TotalQuestion, request.Questions.FirstOrDefault().TestId);
        //        foreach (var question in request.Questions)
        //        {
        //            var checkQuestion = await _repoQuestion.GetQuestionById(question.QuestionId);
        //            if (checkQuestion != null)
        //            {
        //                var updateQuestion = await _repoQuestion.UpdateQuestionInformation(question);
        //                foreach (var option in question.Options)
        //                {
        //                    var checkOption = await _repoOption.UpdateOptionInformation(option);
        //                    if (checkOption != null)
        //                    {
        //                        var updateOption = await _repoOption.UpdateOptionInformation(option);
        //                    }
        //                    else
        //                    {
        //                        var newOption = _mapper.Map<OptionRequest>(option);
        //                        var check = await _repoOption.CreateOption(updateQuestion, newOption);
        //                        if (!check)
        //                        {
        //                            return Ok(new HttpStatusCodeResponse(204));
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                var newQuestion = _mapper.Map<QuestionRequest>(question);
        //                var addQuestion = await _repoQuestion.CreateQuestion(newQuestion);
        //                foreach (var opt in newQuestion.Options)
        //                {
        //                    var check = await _repoOption.CreateOption(addQuestion, opt);
        //                    if (!check)
        //                    {
        //                        return Ok(new HttpStatusCodeResponse(204));
        //                    }
        //                }
        //            }
        //        }
        //        return Ok(new HttpStatusCodeResponse(200));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }
        //}
    }
}
