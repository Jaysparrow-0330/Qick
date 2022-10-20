using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        // get list test from database by user
        Task<IEnumerable<Question>> GetListQuestionBasedOnTestId(int testId);


    }
}
