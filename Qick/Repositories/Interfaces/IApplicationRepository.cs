using Qick.Dto.Requests;
using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IApplicationRepository
    {
        // create Acadamic Profile
        Task<Application> CreateApplication(CreateApplicationRequest request);

        // create Acadamic Profile
        Task<Application> ChangeStatusApplication(String status, Guid? AppId);

        // create 
        Task<ApplicationDetail> CreateApplicationDetail(CreateApplicationDetailRequest request);

        // get 
        Task<IEnumerable<Application>> GetApplicationByUniId(Guid? uniId);
        // get 
        Task<IEnumerable<Application>> GetApplicationByUserId(Guid? userId);

        // get application detail
        Task<Application> GetApplicationDetail(Guid? appId);
    }
}
