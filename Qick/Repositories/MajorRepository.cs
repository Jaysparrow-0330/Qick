using Microsoft.EntityFrameworkCore;
using Qick.Dto.Enum;
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
                    .Where(a=> a.Specializations.Where(x => x.UniversitySpecializations.ToList().Count() > 0).ToList().Count>0)
                    .ToListAsync();
                return response;
            }
            catch (Exception ex)    
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Major>> GetAllMajorByAdmin()
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
        public async Task<Major> GetMajorById(Guid majorId)
        {
            try
            {
                var response = await _context.Majors
                    .Where(a => a.Id == majorId )
                    .FirstOrDefaultAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Specialization> GetSpecById(Guid specId)
        {
            try
            {
                var response = await _context.Specializations
                    .Where(a => a.Id == specId)
                    .FirstOrDefaultAsync();
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
                    .Where(x => x.Id == x.JobMajors
                    .Where(a => a.JobId == JobId)
                    .FirstOrDefault().MajorId &&  x.Specializations.Where(x => x.UniversitySpecializations.ToList().Count() > 0).ToList().Count > 0)
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Major>> GetMajorByUniId(Guid uniId)
        {
            try
            {
                var response = await _context.Majors
                    .Where(x => x.Specializations.Where(u => u.UniversitySpecializations.Where(i => i.UniId == uniId).Count() > 0).Count() > 0)
                    .Include(p => p.Specializations.Where(a => a.UniversitySpecializations.Where(x => x.UniId == uniId).Count() > 0))
                    .ThenInclude(u => u.UniversitySpecializations.Where(a => a.UniId == uniId))
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Major>> GetMajorByUniIdTest(Guid uniId)
        {
            try
            {
                var response = await _context.Majors
                    .Where(x => x.Specializations.Where(u => u.UniversitySpecializations.Where(i => i.UniId == uniId).Count()>0).Count() > 0) 
                    .Include(p => p.Specializations.Where(a => a.UniversitySpecializations.Where(x => x.UniId == uniId).Count()>0))
                    .ThenInclude(u => u.UniversitySpecializations.Where(a => a.UniId == uniId))
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Specialization>> GetSpecByMajorId(Guid? MajorId)
        {
            try
            {
                var response = await _context.Specializations
                    .Where(x => x.MajorId == MajorId )
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
