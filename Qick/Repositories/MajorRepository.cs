using Microsoft.EntityFrameworkCore;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class MajorRepository : IMajorRepository
    {
        private readonly QickDatabaseManangementContext _context;
        public MajorRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Major>> GetAllMajor()
        {
            try
            {
                var response = await _context.Majors
                    .ToListAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<IEnumerable<Major>> GetMajorByJobId(int JobId)
        {
            throw new NotImplementedException();
        }
    }
}
