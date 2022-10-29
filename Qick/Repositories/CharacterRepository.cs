using Microsoft.EntityFrameworkCore;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly QickDatabaseManangementContext _context;
        public CharacterRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }

        public async Task<Character> GetCharacterResult(int testId, string? resultShortName)
        {
            try
            {
                var result = await _context.Characters
                             .Where(a => a.TestId == testId && a.ResultShortName.Equals(resultShortName))
                             .FirstOrDefaultAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IEnumerable<Character>> GetAllCharacterResult(int testId)
        {
            try
            {
                var result = await _context.Characters
                                    .Where(a => a.TestId == testId)
                                    .ToListAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
