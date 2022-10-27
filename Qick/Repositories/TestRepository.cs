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
                .Include(i => i.Creator)
                .Include(u => u.QuizType)
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
                .Include(i => i.Creator)
                .Include(u => u.QuizType)
                .FirstOrDefaultAsync();
            return testDetail;
        }

        public async Task<Test> GetTestToAttempForUser(int testId,Guid userId)
        {
            var testMember = await _context.Tests
                .Where(a => a.Id == testId)
                .Include(u => u.Questions.Where(a => a.Status == Status.ACTIVE))
                .ThenInclude(q => q.Options.Where(a => a.Status == Status.ACTIVE))
                .Where(a => a.Status == Status.ACTIVE)
                .FirstOrDefaultAsync();
            return testMember;
        }

        public async Task<Test> GetTestToAttempForGuest(int testId)
        {
            var testMember = await _context.Tests   
                .Where(a => a.Id == testId)
                .Include(u => u.Questions.Where(a => a.Status == Status.ACTIVE))
                .ThenInclude(q => q.Options.Where(a => a.Status == Status.ACTIVE))
                .Where(a => a.Status == Status.ACTIVE)
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

        public async Task<IEnumerable<Models.TestType>> GetActiveTestType()
        {
            var listTestType = await _context.TestTypes
                .Where(u => u.Status == Status.ACTIVE)
                .ToListAsync();

            return listTestType;
        }

        public async Task<IEnumerable<Test>> GetListAllTest(Guid userId)
        {
            var testMember = await _context.Tests
                .Include(u => u.QuizType)
                .Include(i => i.Creator)
                .ToListAsync();

            return testMember;
        }

        public async Task<bool> CreateResult(ResultRequest request)
        {
            try
            {
                Character addResult = new()
                {
                   Id =Guid.NewGuid(),
                   TestId = request.TestId,
                   ResultName = request.ResultName,
                   ResultRelationship = request.ResultRelationship,
                   ResultSuccessRule   = request.ResultSuccessRule,
                   ResultSummary = request.ResultSummary,
                   ResultShortName = request.ResultShortName,
                   ResultPicture = request.ResultPicture,
                   ResultCareer = request.ResultCareer
                };
                await _context.Characters.AddAsync(addResult);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Test> UpdateTestInformation(UpdateTestRequest test)
        {
            try
            {
                var testDb = await _context.Tests
                    .Where(u => u.Id == test.Id)
                    .FirstOrDefaultAsync();
                if (testDb != null)
                {
                    testDb.QuizName = test.QuizName;
                    testDb.TotalQuestion = test.TotalQuestion;
                    testDb.Introduction = test.Introduction;
                    testDb.History = test.History;
                    testDb.CriteriaInformation = test.CriteriaInformation;
                    testDb.BannerUrl = test.BannerUrl;
                    testDb.BackgroundUrl = test.BackgroundUrl;
                } 
                else
                {
                    { throw new Exception("Test does not exist"); }
                }

                await _context.SaveChangesAsync();
                return testDb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Character> CalculateTestResult(CalculateResultRequest request)
        {
            try
            {
                var type = await _context.Tests
                    .Where(i => i.Id == request.TestId)
                    .Include(u => u.QuizType)
                    .FirstOrDefaultAsync();

                if (type.QuizType.QuizTypeName.ToLower().Equals("mbti"))
                {

                }
                var result = await _context.Characters
                    .FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
