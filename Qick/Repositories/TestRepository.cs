using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Test>> GetListTest(Guid userId)
        {
            var testMember = await _context.Tests
                .Where(u => u.Status == true)
                .ToListAsync();

            return testMember;
        }

        public async Task<IEnumerable<Test>> GetListTestGuest()
        {
            var testMember = await _context.Tests
                  .Where(u => u.Status == true)
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
    }
}
