using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IMajorRepository
    {
        // get all Major
        Task<IEnumerable<Major>> GetAllMajor();

        // get Jobs by character Id
        Task<IEnumerable<Major>> GetMajorByJobId(int JobId);

        // get Specs by Major Id
        Task<IEnumerable<Specialization>> GetSpecByMajorId(int MajorId);

        // get all Major
        Task<IEnumerable<Specialization>> GetAllSpecDb();
    }
}
