using Qick.Dto.Requests;
using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IApplicationRepository
    {
        // create Acadamic Profile
        Task<Application> CreateApplication(CreateApplicationRequest request);

        // create 
        Task<ApplicationDetail> CreateApplicationDetail(CreateApplicationDetailRequest request);

        // get 
        Task<IEnumerable<Application>> GetApplication(Guid? uniId);
    }
}
