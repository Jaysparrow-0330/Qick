using Qick.Dto.Requests;
using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IUniversityRepository
    {
        // get list all university by status
        Task<IEnumerable<University>> GetListAllUniversity(string status);

        // create University  by admin or godad
        Task<bool> CreateUniversity(CreateUniversityRequest request);

        // create University  by admin or godad
        Task<bool> CreateUniversitySpec(CreateUniSpecRequest request);
    }
}
