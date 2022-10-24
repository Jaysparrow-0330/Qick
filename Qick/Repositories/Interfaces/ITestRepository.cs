using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface ITestRepository
    {

        // get list active test from database by user
        Task<IEnumerable<Test>> GetListActiveTest(Guid userId);

        // get list all status test from database by user
        Task<IEnumerable<Test>> GetListAllTest(Guid userId);

        // get list active test from database by guest
        Task<IEnumerable<Test>> GetListActiveTestGuest();

        // get test  user to attemp
        Task<Test> GetTestToAttempForUser(int testId, Guid userId);

        // get test  user to attemp
        Task<Test> GetTestToAttempForGuest(int testId);

        // get test  by testId
        Task<Test> GetTestById(int testId);

        // create test  by creatorId ony admin or godad
        Task<Test> CreateTest(CreateTestRequest test, Guid userId);

        // create result  by test Id ony admin or godad
        Task<bool> CreateResult(ResultRequest request);

        // create result  by test Id ony admin or godad
        Task<Character> CalculateTestResult(CalculateResultRequest request);

        // get all testType
        Task<IEnumerable<Models.TestType>> GetActiveTestType();

    }
}
