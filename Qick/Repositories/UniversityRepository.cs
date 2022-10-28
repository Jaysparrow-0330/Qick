using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly QickDatabaseManangementContext _context;

        public UniversityRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<University>> GetListAllUniversity(string status)
        {
            throw new NotImplementedException();
        }
    }
}
