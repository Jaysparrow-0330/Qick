using Microsoft.EntityFrameworkCore;
using Qick.Dto.Enum;
using Qick.Dto.Requests;
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

        public async Task<Question> CreateQuestion(CreateQuestionRequest request)
        {
            try
            {
                Question question = new()
                {
                    TestId = request.TestId,
                    QuestionContent = request.QuestionContent,
                    QuestionTypeId =   request.QuestionTypeId,
                    CreatedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    Value = request.Value
                };
                
                await _context.Questions.AddAsync(question);
                await _context.SaveChangesAsync();
                return question;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Question>> GetListQuestionBasedOnTestId(int testId)
        {
            try
            {
                var questions = _context.Questions
                    .Where(m => m.TestId == testId && m.Status == Status.ACTIVE)
                    .Include(i => i.Options)
                    .ToListAsync();
                return await questions;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<IEnumerable<QuestionType>> GetActiveQuestionType()
        {
            var listQuestionType = await _context.QuestionTypes
                .Where(u => u.Status == Status.ACTIVE)
                .ToListAsync();

            return listQuestionType;
        }

        public async Task<Question> GetQuestionById(int questionId)
        {
            var questionDetail = await _context.Questions
                .Where(a => a.Id == questionId)
                .FirstOrDefaultAsync();
            return questionDetail;
        }

        public Task<bool> UpdateQuestionInformation(UpdateQuestionRequest question)
        {
            throw new NotImplementedException();
        }
    }
}
