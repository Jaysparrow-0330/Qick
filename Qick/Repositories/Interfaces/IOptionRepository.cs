using Qick.Dto.Requests;
using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IOptionRepository
    {

        // get list option from database by user
        Task<IEnumerable<Option>> GetListOptionBasedOnQuestionId(int questionId);

        // create option  by question id ony admin or godad
        Task<bool> CreateOption(Question question, CreateOptionRequest opt);

        // get option  by optionId
        Task<Option> GetOptionById(int optionId);

        // update option
        Task<bool> UpdateOptionInformation(UpdateOptionRequest option);
    }
}
