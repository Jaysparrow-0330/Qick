using Qick.Dto.Requests;

namespace Qick.Repositories.Interfaces
{
    public interface ISystemRepository
    {
        // create Job 
        Task<bool> CreateJob(JobRequest request);
    }
}
