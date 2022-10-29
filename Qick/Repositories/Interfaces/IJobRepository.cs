using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IJobRepository
    {
        // get all Job 
        Task<IEnumerable<Job>> GetAllJob();
    }
}
