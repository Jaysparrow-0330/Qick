using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface ITestRepository
    {
        /// <summary>
        /// get list test from database by user
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Test>> GetListTest(string userId);


        /// <summary>
        /// get list test from database by guest
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Test>> GetListTestGuest();
    }
}
