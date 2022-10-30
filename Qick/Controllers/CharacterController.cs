using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Responses;
using Qick.Repositories.Interfaces;

namespace Qick.Controllers
{
    [Authorize]
    [Route("api/character")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRepository _repo;
        private readonly IMapper _mapper;
        public CharacterController(ICharacterRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // Get character result by character id
        [AllowAnonymous]
        [HttpGet("get-character")]
        public async Task<IActionResult> GetCharacterResult(int testId, string? resultShortName)
        {
            try
            {
                if (resultShortName != null)
                {
                    var character = await _repo.GetCharacterResult(testId, resultShortName);
                    var characterResponse = _mapper.Map<ResultResponse>(character);
                    return Ok(characterResponse);
                }
                else
                {
                    var listCharacter = await _repo.GetAllCharacterResult(testId);
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
    }
}
