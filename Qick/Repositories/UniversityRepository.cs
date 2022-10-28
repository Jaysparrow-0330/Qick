using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly QickDatabaseManangementContext _context;

        public UniversityRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUniversity(CreateUniversityRequest request)
        {
            try
            {
                University addUni = new()
                {
                    Id = Guid.NewGuid(),
                    UniName = request.UniName,
                    UniCode = request.UniCode,
                    Email = request.Email,
                    AddressNumber = request.AddressNumber,
                    Phone = request.Phone,
                    AvatarUrl = request.AvatarUrl,
                    CoverPhotoUrl = request.CoverPhotoUrl,
                    WebsiteUrl = request.WebsiteUrl,
                    Description = request.Description,
                    CreatedDate = DateTime.Now,
                    Status = Status.ACTIVE
                };
                await _context.Universities.AddAsync(addUni);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<IEnumerable<University>> GetListAllUniversity(string status)
        {
            throw new NotImplementedException();
        }
    }
}
