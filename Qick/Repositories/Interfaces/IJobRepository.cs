using Qick.Dto.Requests;
using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IJobRepository
    {
        // get all Job 
        Task<IEnumerable<Job>> GetAllJob();

        // get Jobs by character Id
        Task<IEnumerable<Job>> GetJobByCharacterId(Guid CharacterId);
        Task<IEnumerable<Job>> GetJobByCharacterIdByAdmin(Guid? characterId);
        Task<IEnumerable<Job>> GetAllJobByAdmin();
        Task<Job> UpdateJob(UpdateJobRequest request);
        Task<IEnumerable<Job>> GetAttemptForFilter(Guid? userId);
    }
}
