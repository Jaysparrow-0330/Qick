using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qick.Dto.Requests;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly QickDatabaseManangementContext _context;
        private readonly IMapper _mapper;
        public JobRepository(QickDatabaseManangementContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<Job> UpdateJob(UpdateJobRequest request)
        {
            try
            {
                //find the job record
                var response = await _context.Jobs
                    .Where(x=>x.Id== request.Id)
                    .Include(x => x.JobMajors)
                    .FirstOrDefaultAsync();
                if (response == null) throw new Exception("Job not found") ;
                // remove its foreign key
                _context.RemoveRange(response.JobMajors);
                // reassign the foreign key
                _mapper.Map(request, response);

                // there will be a situation where its doesn't change any content.
                if( await _context.SaveChangesAsync() <= 0) throw new Exception("Nothing changes");

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
