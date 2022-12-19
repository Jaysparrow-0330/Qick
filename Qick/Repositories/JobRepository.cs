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
        public async Task<IEnumerable<Job>> GetJobByCharacterIdByAdmin(Guid? characterId)
        {
            try
            {
                var response = await _context.Jobs
                    .Where(x => x.Id == x.JobMappings
                    .Where(a => a.CharacterId == characterId)
                    .FirstOrDefault().JobId)
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Job>> GetAttemptForFilter(Guid? userId)
        {
            var resultMbti = await _context.Attempts
                .Where(u => u.UserId == userId && u.Test.TestTypeId == 3)
                .OrderByDescending(x => x.AttemptDate)
                .FirstOrDefaultAsync();
            var charMbti = await _context.Characters
                             .Where(a => a.TestId == resultMbti.TestId && a.ResultShortName.Equals(resultMbti.ResultShortName))
                             .FirstOrDefaultAsync();
            var resultJobMbti = await _context.Jobs
                    .Where(x => x.Id == x.JobMappings
                    .Where(a => a.CharacterId == charMbti.Id)
                    .FirstOrDefault().JobId && x.JobMajors.ToList().Count() > 0)
                    .ToListAsync();

            var resultDisc = await _context.Attempts
                .Where(u => u.UserId == userId && u.Test.TestTypeId == 4)
                .OrderByDescending(x => x.AttemptDate)
                .FirstOrDefaultAsync();
            var charDisc = await _context.Characters
                             .Where(a => a.TestId == resultDisc.TestId && a.ResultShortName.Equals(resultDisc.ResultShortName))
                             .FirstOrDefaultAsync();
            var resultJobDisc= await _context.Jobs
                    .Where(x => x.Id == x.JobMappings
                    .Where(a => a.CharacterId == charDisc.Id)
                    .FirstOrDefault().JobId && x.JobMajors.ToList().Count() > 0)
                    .ToListAsync();

            var result = resultJobMbti.Intersect(resultJobDisc);
            if(!(result.Count()>0))
            {
                var response = resultJobMbti.Concat(resultJobDisc);
                return response;
            }
            return result;
          
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
