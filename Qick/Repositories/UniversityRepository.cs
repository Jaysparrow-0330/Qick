using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> CreateUniversitySpec(CreateUniSpecRequest request)
        {
            try
            {
                    UniversitySpecialization addUniSpec = new()
                    {
                       UniId = request.UniId,
                       SpecId = request.SpecId,
                       UniSpecName =   request.UniSpecName,
                       SpecCode = request.SpecCode,
                       Status = Status.ACTIVE,

                    };
                    
                await _context.UniversitySpecializations.AddAsync(addUniSpec);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<University>> GetListAllUniversity(string status)
        {
            try
            {
                var response = await _context.Universities
                    .Where(a => a.Status.Equals(status))
                    .ToListAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
