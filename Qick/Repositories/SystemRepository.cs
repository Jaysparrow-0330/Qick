using Microsoft.EntityFrameworkCore;
using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class SystemRepository : ISystemRepository
    {
        private readonly QickDatabaseManangementContext _context;

        public SystemRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateJob(JobRequest request)
        {
            try
            {
                Job addJob = new() 
                {
                    JobName = request.JobName,
                    Status = Status.ACTIVE,
                    Description = request.Description
                };
                await _context.Jobs.AddAsync(addJob);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateJobCharMapping(JobCharMappingRequest request)
        {
            try
            {
                foreach (var Id in request.JobId)
                {
                    JobMapping addJobMapping = new()
                    {
                        CharacterId = request.CharacterId,
                        JobId = Id,
                        Status = Status.ACTIVE,
                    };
                    await _context.JobMappings.AddAsync(addJobMapping);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateMajor(MajorRequest request)
        {
            try
            {
                Major addMajor = new()
                {
                    Id = Guid.NewGuid(),
                    MajorName = request.MajorName,
                    Status = Status.ACTIVE,
                    Description = request.Description,
                    MajorCode = request.MajorCode
                };
                await _context.Majors.AddAsync(addMajor);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
