using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IOptionRepository
    {

        // get list option from database by user
        Task<IEnumerable<Option>> GetListOptionBasedOnQuestionId(int questionId);
    }
}
