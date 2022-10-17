using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Test>> GetListTest(string userId)
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
    }
}
