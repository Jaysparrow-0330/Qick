using Microsoft.EntityFrameworkCore;
using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly QickDatabaseManangementContext _context;

        public TestRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Test>> GetListActiveTest(Guid userId)
        {
            var testMember = await _context.Tests
                .Where(u => u.Status == Status.ACTIVE)
                .ToListAsync();

            return testMember;
        }

        public async Task<IEnumerable<Test>> GetListActiveTestGuest()
        {
            var testMember = await _context.Tests
                  .Where(u => u.Status == Status.ACTIVE)
                  .ToListAsync();
            return testMember;
        }

        public async Task<Test> GetTestById(int testId)
        {
            var testDetail = await _context.Tests
                .Where(a => a.Id == testId)
                .FirstOrDefaultAsync();
            return testDetail;
        }

        public async Task<Test> GetTestToAttempForUser(int testId,Guid userId)
        {
            var testMember = await _context.Tests
                .Where(a => a.Id == testId)
                .Include(u => u.Questions).ThenInclude(q => q.Options)
                .FirstOrDefaultAsync();
            return testMember;
        }

        public async Task<Test> GetTestToAttempForGuest(int testId)
        {
            var testMember = await _context.Tests   
                .Where(a => a.Id == testId)
                .Include(u => u.Questions).ThenInclude(q => q.Options)
                .FirstOrDefaultAsync();
            return testMember;
        }

        public async Task<Test> CreateTest(CreateTestRequest request, Guid userId)
        {
            try
            {
                Test test = new()
                {
                    CreatorId = userId,
                    QuizName = request.QuizName,
                    QuizTypeId = request.QuizTypeId,
                    TotalQuestion = request.TotalQuestion,
                    CreatedDate = DateTime.Now,
                    Introduction =  request.Introduction,
                    History = request.History,
                    CriteriaInformation = request.CriteriaInformation,
                    BannerUrl = request.BannerUrl,
                    BackgroundUrl = request.BackgroundUrl, 
                    Status = Status.PENDING
                };
                await _context.Tests.AddAsync(test);
                await _context.SaveChangesAsync();
                return test;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Models.TestType>> GetTestType()
        {
            var listTestType = await _context.TestTypes
                .Where(u => u.Status == true)
                .ToListAsync();

            return listTestType;
        }

        public async Task<IEnumerable<Test>> GetListAllTest(Guid userId)
        {
            var testMember = await _context.Tests
                .ToListAsync();

            return testMember;
        }
    }
}
