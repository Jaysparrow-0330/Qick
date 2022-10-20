using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface ITestRepository
    {

        // get list test from database by user
        Task<IEnumerable<Test>> GetListTest(Guid userId);

        // get list test from database by guest
        Task<IEnumerable<Test>> GetListTestGuest();

        // get test  user to attemp
        Task<Test> GetTestToAttempForUser(int testId, Guid userId);

        // get test  user to attemp
        Task<Test> GetTestToAttempForGuest(int testId);

        // get test  by testId
        Task<Test> GetTestById(int testId);
    }
}
