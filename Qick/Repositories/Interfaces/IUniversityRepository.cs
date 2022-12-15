using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IUniversityRepository
    {
        // get list all university by status
        Task<IEnumerable<University>> GetListAllUniversity(string? status);

        // get list all university by status
        Task<IEnumerable<UniversitySpecialization>> GetListAllUniversitySpec(Guid? UniId);

        // get list all university by status
        Task<IEnumerable<University>> GetUniversityByMajorId(Guid majorId);

        // create University  by admin or godad
        Task<bool> CreateUniversity(CreateUniversityRequest request);

        // create University  by admin or godad
        Task<bool> CreateUniversitySpec(CreateUniSpecRequest request);

        // get university detail
        Task<University> GetUniversityDetail(Guid? uniId);
        Task<University> UpdateUni(UpdateUniRequest request, Guid uniId);
        Task<University> GetUniversityById(Guid? uniId);
        Task<University> BanUni(Guid uniId);
        Task<DashboardUniResponse> GetDashboardUni(Guid uniId);

    }
}
