using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        /// <summary>
        /// get list test from database by user
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Question>> GetListQuestionBasedOnTestId(int testId);


    }
}
