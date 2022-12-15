using Microsoft.EntityFrameworkCore;
using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;
using Qick.Repositories.Interfaces;
using System.Linq;

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
                    WardId = request.WardId,
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

        public async Task<IEnumerable<University>> GetListAllUniversity(string? status)
        {
            try
            { if(status != null)
                {
                    var response = await _context.Universities
                    .Where(a => a.Status.Equals(status))
                    .ToListAsync();
                    return response;
                } 
                else
                {
                    var response = await _context.Universities
                                        .ToListAsync();
                    return response;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<UniversitySpecialization>> GetListAllUniversitySpec(Guid? UniId)
        {
            try
            {
                    var response = await _context.UniversitySpecializations
                    .Where(a => a.UniId == UniId)
                    .ToListAsync();
                    return response;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public async Task<IEnumerable<University>> GetUniversityByMajorId(Guid majorId)
        {
            try
            {
                    var listSpec = await _context.Specializations
                    .Where(a => a.MajorId == majorId && a.UniversitySpecializations.ToList().Count() > 0)
                    .Select(r => r.Id)
                    .ToListAsync();
                var responseList = new List<University>();

                foreach (var id in listSpec)
                {
                    var response = await _context.Universities
                    .Where(a => a.Id == a.UniversitySpecializations.Where(n => n.SpecId == id).FirstOrDefault().UniId)
                    .ToListAsync();
                    foreach (var item in response)
                    {
                        if (!responseList.Contains(item))
                        {
                            responseList.Add(item);
                        }
                    }
                    
                }
                

                    return responseList;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<University> GetUniversityDetail(Guid? uniId)
        {
            try
            {
                var result = await _context.Universities
                             .Where(a => a.Id == uniId)
                             .Include(a => a.Ward)
                             .ThenInclude(u => u.District)
                             .ThenInclude(u => u.Province)
                             .Where(u => u.WardId == u.Ward.Id)
                             .FirstOrDefaultAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<DashboardUniResponse> GetDashboardUni(Guid uniId)
        {
            try
            {
                var user = await _context.Users
                    .Where(u => u.Status == Status.ACTIVE && u.RoleId == Roles.STAFF && u.UniversityId == uniId)
                    .ToListAsync();

                var News = await _context.AddmissionNews
                    .Where(u => u.Status == Status.ACTIVE)
                    .ToListAsync();

                var applicaiton = await _context.Applications
                    .Where(u => u.Status == Status.ACTIVE && u.UniId == uniId)
                    .ToListAsync();

                var save = await _context.SavedUnis
                    .Where(u => u.Status == Status.ACTIVE && u.UniversityId == uniId)
                    .ToListAsync();

                DashboardUniResponse response = new()
                {
                    totalApplication = applicaiton.Count(),
                    totalNews = News.Count(),
                    totalSaveUni = save.Count(),
                    totalStaff = user.Count()
                };
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<University> GetUniversityById(Guid? uniId)
        {
            try
            {
                var result = await _context.Universities
                             .Where(a => a.Id == uniId)
                             .FirstOrDefaultAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> CheckUniSaved(Guid uniId, Guid userId)
        {
            try
            {
                var result = await _context.SavedUnis
                             .Where(a => a.UserId == userId && a.UniversityId == uniId)
                             .FirstOrDefaultAsync();
                if (result == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<University> UpdateUni(UpdateUniRequest request, Guid uniId)
        {
            try
            {
                var uni = await _context.Universities
                    .Where(u => u.Id == uniId)
                    .FirstOrDefaultAsync();

                if (uni != null)
                {
                    uni.UniName = request.UniName;
                    uni.AddressNumber = request.AddressNumber;
                    uni.Phone = request.Phone;
                    uni.Email = request.Email;
                    uni.WebsiteUrl = request.WebsiteUrl;
                    uni.AvatarUrl = request.AvatarUrl;
                    uni.CoverPhotoUrl = request.CoverPhotoUrl;
                    uni.UniCode = request.UniCode;
                    uni.Description = request.Description;
                    uni.WardId = request.WardId;
                }
                else
                {
                    { throw new Exception("University does not exist"); }
                }

                await _context.SaveChangesAsync();
                return uni;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<University> BanUni(Guid uniId)
        {
            try
            {
                var uni = await _context.Universities
                    .Where(u => u.Id == uniId)
                    .FirstOrDefaultAsync();

                if (uni != null)
                {
                    if (uni.Status == Status.ACTIVE)
                    {
                        uni.Status = Status.BANNED;
                    }
                    else if (uni.Status == Status.BANNED)
                    {
                        uni.Status = Status.ACTIVE;
                    }

                }
                else
                {
                    { throw new Exception("University does not exist"); }
                }

                await _context.SaveChangesAsync();
                return uni;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
