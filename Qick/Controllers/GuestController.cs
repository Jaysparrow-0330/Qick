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
        [HttpGet("get-active-test")]
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

        // Get test to attemp by guest
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

        // Get character result by character id
        [HttpGet("get-character")]
        public async Task<IActionResult> GetCharacterResult(Guid? characterId)
        {
            try
            {
                

                if(characterId != null)
                {
                    var character = await _repo.GetCharacterResult(characterId);
                     var characterResponse = _mapper.Map<ResultResponse>(character);
                    return Ok(characterResponse);
                }
                else
                {
                    var listCharacter = await _repo.GetAllCharacterResult();
                     var characterResponse = _mapper.Map<IEnumerable<ResultResponse>>(listCharacter);
                    return Ok(characterResponse);
                }
                
                return Ok(new HttpStatusCodeResponse(204));
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

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
