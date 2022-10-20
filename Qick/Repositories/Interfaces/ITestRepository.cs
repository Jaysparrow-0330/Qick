using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface ITestRepository
    {
        /// <summary>
        /// get list test from database by user
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Test>> GetListTest(Guid userId);


        /// <summary>
        /// get list test from database by guest
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Test>> GetListTestGuest();

        /// <summary>
        /// get test  user to attemp
        /// </summary>
        /// <returns></returns>
        Task<Test> GetTestToAttempForUser(int testId, Guid userId);

        /// <summary>
        /// get test  user to attemp
        /// </summary>
        /// <returns></returns>
        Task<Test> GetTestToAttempForGuest(int testId);
        /// <summary>
        /// get test  by testId
        /// </summary>
        /// <returns></returns>
        Task<Test> GetTestById(int testId);
    }
}
