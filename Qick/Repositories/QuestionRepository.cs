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

        public async Task<Question> CreateQuestion(QuestionRequest request)
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

        public async Task<IEnumerable<Question>> GetListAllQuestionBasedOnTestId(int testId)
        {
            try
            {
                var questions = _context.Questions
                    .Where(m => m.TestId == testId)
                    .Include(i => i.Options)
                    .ToListAsync();
                return await questions;
            }
            catch (Exception ex) { throw ex; }
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

        public async Task<Question> GetQuestionById(int? questionId)
        {
            try
            {
                var questionDetail = await _context.Questions
                                .Where(a => a.Id == questionId)
                                .FirstOrDefaultAsync();
                if (questionDetail != null)
                {
                    return questionDetail;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<Question> UpdateQuestionInformation(QuestionRequest question)
        {
            try
            {
                var questionDb = await _context.Questions
                    .Where(u => u.Id == question.QuestionId)
                    .FirstOrDefaultAsync(); 

                if (questionDb != null)
                {
                    questionDb.QuestionContent = question.QuestionContent;
                    questionDb.QuestionTypeId = question.QuestionTypeId;
                    questionDb.Value = question.Value;
                    questionDb.Status = question.Status;
                }
                else
                {
                    { throw new Exception("Question does not exist"); }
                }

                await _context.SaveChangesAsync();
                return questionDb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
