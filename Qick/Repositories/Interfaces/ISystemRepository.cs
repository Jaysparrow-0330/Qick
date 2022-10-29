using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface ISystemRepository
    {
        // create Job 
        Task<bool> CreateJob(JobRequest request);

        // create Major
        Task<bool> CreateMajor(MajorRequest request);

        // create Mapping Job
        Task<bool> CreateJobCharMapping(JobCharMappingRequest request);
    }
}
