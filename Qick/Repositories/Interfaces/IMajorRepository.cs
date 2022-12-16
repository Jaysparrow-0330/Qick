using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IMajorRepository
    {
        // get all Major
        Task<IEnumerable<Major>> GetAllMajor();

        // get Jobs by character Id
        Task<IEnumerable<Major>> GetMajorByJobId(int JobId);

        // get Jobs by character Id
        Task<IEnumerable<Major>> GetMajorByUniId(Guid uniId);
        Task<IEnumerable<Major>> GetAllMajorByAdmin();

        // get Specs by Major Id
        Task<IEnumerable<Specialization>> GetSpecByMajorId(Guid? MajorId);

        // get all Major
        Task<IEnumerable<Specialization>> GetAllSpecDb();

        Task<Major> GetMajorById(Guid majorId);
        Task<Specialization> GetSpecById(Guid specId);
    }
}
