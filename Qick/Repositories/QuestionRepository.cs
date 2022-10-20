using Microsoft.EntityFrameworkCore;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QickDatabaseManangementContext _context;

        public QuestionRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Question>> GetListQuestionBasedOnTestId(int testId)
        {
            try
            {
                var questions = _context.Questions
                    .Where(m => m.TestId == testId && m.Status == true);
                return await questions.ToListAsync();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
