using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface ICharacterRepository
    {

        // get character result
        Task<Character> GetCharacterResult(int testId, string? resultShortName);

        // get all character result
        Task<IEnumerable<Character>> GetAllCharacterResult(int testId);

    }
}
