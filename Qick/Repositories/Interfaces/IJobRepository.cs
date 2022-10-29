using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IJobRepository
    {
        // get all Job 
        Task<IEnumerable<Job>> GetAllJob();

        // get Jobs by character Id
        Task<IEnumerable<Job>> GetJobByCharacterId(Guid CharacterId);
    }
}
