using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IUniversityRepository
    {
        // get list all university by status
        Task<IEnumerable<University>> GetListAllUniversity(string status);
    }
}
