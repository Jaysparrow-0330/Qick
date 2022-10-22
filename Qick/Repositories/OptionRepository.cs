using Microsoft.EntityFrameworkCore;
using Qick.Dto.Enum;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        private readonly QickDatabaseManangementContext _context;

        public OptionRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Option>> GetListOptionBasedOnQuestionId(int questionId)
        {
            try
            {
                var options = _context.Options
                    .Where(m => m.QuestionId == questionId && m.Status == Status.ACTIVE).ToListAsync();
                return await options;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
