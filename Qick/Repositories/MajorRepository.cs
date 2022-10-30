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

        public async Task<IEnumerable<Specialization>> GetAllSpecDb()
        {
            try
            {
                var response = await _context.Specializations
                    .ToListAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Major>> GetMajorByJobId(int JobId)
        {
            try
            {
                var response = await _context.Majors
                    .Where(x => x.Id == x.JobMajors.Where(a => a.JobId == JobId).FirstOrDefault().MajorId)
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<IEnumerable<Specialization>> GetSpecByMajorId(int MajorId)
        {
            throw new NotImplementedException();
        }
    }
}
