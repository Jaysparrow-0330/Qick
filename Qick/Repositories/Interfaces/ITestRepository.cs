using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface ITestRepository
    {

        // get list active test from database by user
        Task<IEnumerable<Test>> GetListActiveTest();

        // get list all status test from database by user
        Task<IEnumerable<Test>> GetListAllTest(Guid userId);

        // get list active test from database by guest
        Task<IEnumerable<Test>> GetListActiveTestGuest();

        // get test  user to attemp
        Task<Test> GetTestToAttemp(int testId);

        

        // get test  by testId
        Task<Test> GetTestById(int testId);

        // update test 
        Task<Test> UpdateTestInformation(UpdateTestRequest test);
        // update test 
        Task<Test> UpdateTotalQuestion(int total, int testId);

        // create test  by creatorId ony admin or godad
        Task<Test> CreateTest(CreateTestRequest test, Guid userId);

        // create result  by test Id ony admin or godad
        Task<bool> CreateResult(ResultRequest request);

        // create result  by test Id ony admin or godad
        Task<SubmitResponse> CalculateTestResult(CalculateResultRequest request, Guid? userId);

        // create result  by test Id ony admin or godad
        Task<SubmitResponse> CalculateDiscResult(CalculateResultRequest request, Guid? userId);
        Task<SubmitResponse> CalculateBig5Result(CalculateResultRequest request, Guid? userId);
        Task<SubmitResponse> CalculateHollandResult(CalculateResultRequest request, Guid? userId);
        // get all testType
        Task<IEnumerable<Models.TestType>> GetActiveTestType();
        Task<IEnumerable<ListAttemptResponse>> GetAttempt(Guid? userId);


    }
}
