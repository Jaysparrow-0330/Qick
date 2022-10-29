using Microsoft.EntityFrameworkCore;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly QickDatabaseManangementContext _context;
        public JobRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetAllJob()
        {
            try
            {
                var response = await _context.Jobs
                    .ToListAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
