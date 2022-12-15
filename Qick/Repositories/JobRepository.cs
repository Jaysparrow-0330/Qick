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
                    .Where(a => a.JobMajors.ToList().Count() > 0)
                    .ToListAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Job>> GetAllJobByAdmin()
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
        public async Task<IEnumerable<Job>> GetJobByCharacterId(Guid characterId)
        {
            try
            {
                var response = await _context.Jobs
                    .Where(x => x.Id == x.JobMappings
                    .Where(a => a.CharacterId == characterId)
                    .FirstOrDefault().JobId && x.JobMajors.ToList().Count() > 0)
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
