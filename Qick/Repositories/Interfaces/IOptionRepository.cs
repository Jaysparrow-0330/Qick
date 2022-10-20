using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IOptionRepository
    {
        /// <summary>
        /// get list option from database by user
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Option>> GetListOptionBasedOnQuestionId(int questionId);
    }
}
