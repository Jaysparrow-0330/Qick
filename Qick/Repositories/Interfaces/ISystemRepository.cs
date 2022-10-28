using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface ISystemRepository
    {
        // create Job 
        Task<bool> CreateJob(JobRequest request);

        // get all Job 
        Task<IEnumerable<Job>> GetAllJob();
    }
}
