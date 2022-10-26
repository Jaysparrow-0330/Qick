using Qick.Dto.Requests;
using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        // get list test from database by user
        Task<IEnumerable<Question>> GetListQuestionBasedOnTestId(int testId);

        // create question  by creatorId ony admin or godad
        Task<Question> CreateQuestion(CreateQuestionRequest request);

        // get all questionType
        Task<IEnumerable<QuestionType>> GetActiveQuestionType();

        // get question  by questionId
        Task<Question> GetQuestionById(int questionId);

        // update question
        Task<Question> UpdateQuestionInformation(UpdateQuestionRequest question);

    }
}
