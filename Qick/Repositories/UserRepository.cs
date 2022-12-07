
using Microsoft.EntityFrameworkCore;
using Qick.Dto.Requests;
using Qick.Dto.Enum;
using Qick.Models;
using Qick.Repositories.Interfaces;
using System.Security.Cryptography;
using System.Text;
using Qick.Dto.Exceptions;

namespace Qick.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly QickDatabaseManangementContext _context;
        private readonly IConfiguration _config;
        public UserRepository(QickDatabaseManangementContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        //  Login authentication by email and password
        public async Task<User> Login(LoginRequest login)
        {
            try
            {
                var user = await _context.Users.Where(u => u.Email.Equals(login.Email.ToLower()) && u.RoleId.Equals(Roles.MEMBER) && u.Status != Status.BANNED).FirstOrDefaultAsync();
                if (user != null)
                {
                    if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                        return null;
                    else
                    {
                        if (user.Status == Status.PENDING)
                        {
                            throw new NotActiveException("User Not Active");
                        }
                        else
                        {
                            return user;
                        }

                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> EmailExist(string email)
        {
            try
            {
                if (await _context.Users.Where(a => a.RoleId == Roles.MEMBER).AnyAsync(x => x.Email.Equals(email.ToLower())))
                {
                    return true;
                }   
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> EmailExistUniMa(string email)
        {
            try
            {
                if (await _context.Users.Where(a => a.RoleId == Roles.MANAGER).AnyAsync(x => x.Email.Equals(email.ToLower())))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> EmailExistStaff(string email)
        {
            try
            {
                if (await _context.Users.Where(a => a.RoleId == Roles.STAFF).AnyAsync(x => x.Email.Equals(email.ToLower())))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<User> RegisterUni(ManagerStaffRequest register, string code)
        {
            try
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(code, out passwordHash, out passwordSalt);
                User user = new()
                {
                    Id = Guid.NewGuid(),
                    UniversityId = register.UniversityId,
                    Email = register.Email.ToLower(),
                    UserName = register.Name,
                    SignUpDate = DateTime.Now,
                    RoleId = Roles.MANAGER,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = Status.PENDING,
                    PublicProfile = PublicProfile.INACTIVE
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> RegisterStaff(ManagerStaffRequest register, string code)
        {
            try
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(code, out passwordHash, out passwordSalt);
                User user = new()
                {
                    Id = Guid.NewGuid(),
                    UniversityId = register.UniversityId,
                    Email = register.Email.ToLower(),
                    UserName = register.Name,
                    SignUpDate = DateTime.Now,
                    RoleId = Roles.STAFF,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = Status.PENDING,
                    PublicProfile = PublicProfile.INACTIVE
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<User> Register(RegisterRequest register, string code)
        {
            try
            {
                if(register.Password.Length<8 || register.Password.Length>16) { throw new Exception("Password wrong format"); }
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(register.Password, out passwordHash, out passwordSalt);
                User user = new()
                {
                    Id = Guid.NewGuid(),
                    Email = register.Email.ToLower(),
                    UserName = register.Name,
                    SignUpDate = DateTime.Now,
                    RoleId = Roles.MEMBER,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = Status.PENDING,
                    PublicProfile = PublicProfile.INACTIVE
                };
                CreateOtp(user.Id, code, Status.ETERNAL);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> SaveUni(SaveUniRequest request,Guid userId)
        {
            try
            {
                SavedUni saveUni = new()
                {
                    UserId = userId,
                    UniversityId = request.uniId,
                    Status = Status.ACTIVE
                };
                await _context.SavedUnis.AddAsync(saveUni);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<User>> GetListAllStaff(Guid? UniId)
        {
            try
            {
                var response = await _context.Users
                .Where(a => a.UniversityId == UniId && a.RoleId == Roles.STAFF)
                .ToListAsync();
                return response;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<User>> GetListActiveStaff(Guid? UniId)
        {
            try
            {
                var response = await _context.Users
                .Where(a => a.UniversityId == UniId && a.RoleId == Roles.STAFF && a.Status == Status.ACTIVE)
                .ToListAsync();
                return response;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email.Equals(email.ToLower()) && u.RoleId != Roles.USER_GOOGLE).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SavedUni>> GetAllUniSavedByUserId(Guid userId)
        {
            try
            {
                var list = await _context.SavedUnis
                    .Where(a => a.UserId == userId)
                    .ToListAsync();

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> ActiveUserStatus(User user, string code)
        {
            try
            {
                if (await CheckOtp(user.Id, code, Status.ETERNAL))
                {
                    user.Status = Status.ACTIVE;
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CreateOtp(Guid userId, string code, string status)
        {
            try
            {
                DateTime? valiTime = null;
                if (!status.Equals(Status.ETERNAL))
                {
                    valiTime = DateTime.Now.AddMinutes(10);
                }
                UserOtp otp = new()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.Now,
                    ValidateUntil = valiTime,
                    UserId = userId,
                    Otp = code,
                    Status = status
                };
                _context.UserOtps.AddAsync(otp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<bool> CheckOtp(Guid userId, string code, string status)
        {
            try
            {
                UserOtp otp;
                if (!status.Equals (Status.ETERNAL))
                {
                    otp = await _context.UserOtps.Where(a => a.UserId.Equals(userId) && a.Status != Status.ETERNAL && a.ValidateUntil > DateTime.Now).OrderByDescending(a => a.CreateDate).FirstOrDefaultAsync();
                }
                else
                {
                    otp = await _context.UserOtps.Where(a => a.UserId.Equals(userId) && a.Status == Status.ETERNAL).FirstOrDefaultAsync();
                }
                if (otp != null)
                {
                    if (otp.Otp.Equals(code))
                    {
                        var otps = await _context.UserOtps.Where(a => a.Status == status && a.UserId.Equals(userId)).ToListAsync();
                        _context.UserOtps.RemoveRange(otps);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var hmac = new HMACSHA512(passwordSalt);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            if (hash.SequenceEqual(passwordHash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<User> LoginAd(LoginRequest login)
        {
            try
            {
                var user = await _context.Users
                    .Where(u => u.Email.Equals(login.Email.ToLower()) && (u.RoleId.Equals(Roles.ADMIN)  || u.RoleId.Equals(Roles.MANAGER) || u.RoleId.Equals(Roles.STAFF)) && u.Status != Status.BANNED)
                    .FirstOrDefaultAsync();
                if (user != null)
                {
                    if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                    {
                        return null;
                    } 
                    else
                    {
                        if (user.Status == Status.PENDING)
                        {
                            throw new NotActiveException("Not Active");
                        }
                        else
                        {
                            return user;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<User> LoginUni(LoginRequest login)
        {
            try
            {
                var user = await _context.Users.Where(u => u.Email.Equals(login.Email.ToLower()) && u.RoleId.Equals(Roles.MANAGER) || u.RoleId.Equals(Roles.STAFF) && u.Status != Status.BANNED).FirstOrDefaultAsync();
                if (user != null)
                {
                    if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                    {
                        return null;
                    }
                    else
                    {
                        if (user.Status == Status.PENDING)
                        {
                            throw new NotActiveException("Change Password for the first login");
                        }
                        else
                        {
                            return user;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> UpdateProfile(UserProfileUpdateRequest request, Guid id)
        {
            try
            {
                var user = await _context.Users
                    .Where(u => u.Id == id)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    user.UserName = request.UserName;
                    user.AddressNumber = request.AddressNumber;
                    user.AvatarUrl = request.AvatarUrl;
                    user.CredentialBackImgUrl = request.CredentialBackImgUrl;
                    user.CredentialFrontImgUrl = request.CredentialFrontImgUrl;
                    user.CredentialId = request.CredentialId;
                    user.DateOfBirth = request.DateOfBirth;
                    user.Gender = request.Gender;
                    user.Phone = request.Phone;
                    user.WardId = request.WardId;
                }
                else
                {
                    { throw new Exception("User does not exist"); }
                }

                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AcademicProfile> CreateAcademicProfile(CreateAcademyRequest request,Guid userId)
        {
            try
            {
                AcademicProfile addProfile = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    AcademicRank = request.AcademicRank,
                    AverageScore = request.AverageScore,
                    GraduationYear = request.GraduationYear,
                    HighSchoolId = request.HighSchoolId,
                    SchoolReport1Url = request.SchoolReport1Url,
                    SchoolReport2Url = request.SchoolReport2Url,
                    SchoolReport3Url = request.SchoolReport3Url,
                    SchoolReport4Url =request.SchoolReport4Url
                };
                await _context.AcademicProfiles.AddAsync(addProfile);
                await _context.SaveChangesAsync();
                return addProfile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<User> UpdatePassword(UpdatePassRequest update, User user)
        {
            try
            {
                if (user == null) { throw new Exception("User Not Exist"); }
                if (!VerifyPasswordHash(update.OldPassword, user.PasswordHash, user.PasswordSalt)) { throw new Exception("Wrong password"); }
                if (update.NewPassword.Length < 8 || update.NewPassword.Length > 16) { throw new Exception("Password wrong format"); }
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(update.NewPassword, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                if (user.Status.Equals(Status.PENDING))
                {
                    user.Status = Status.ACTIVE;
                }
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<AcademicProfile> UpdateAcademicProfile(UpdateAcademyRequest request)
        {
            try
            {
                var profile = await _context.AcademicProfiles
                    .Where(u => u.Id == request.Id)
                    .FirstOrDefaultAsync();

                if (profile != null)
                {
                    profile.AcademicRank = request.AcademicRank;
                    profile.AverageScore = request.AverageScore;
                    profile.GraduationYear = request.GraduationYear;
                    profile.HighSchoolId = request.HighSchoolId;
                    profile.SchoolReport1Url = request.SchoolReport1Url;
                    profile.SchoolReport2Url = request.SchoolReport2Url;
                    profile.SchoolReport3Url = request.SchoolReport3Url;
                    profile.SchoolReport4Url = request.SchoolReport4Url;
                }
                else
                {
                    { throw new Exception("Profile does not exist"); }
                }

                await _context.SaveChangesAsync();
                return profile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AcademicProfile> GetAcademicProfile(Guid? UserId)
        {
            try
            {
                var AcaProfile = await _context.AcademicProfiles
                    .Where(a => a.User.Id == UserId)
                    .FirstOrDefaultAsync();
                    return AcaProfile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<User> GetUserById(Guid UserId)
        {
            try
            {
                var user = await _context.Users
                    .Where(a => a.Id == UserId)
                    .FirstOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<User> GetProfile(Guid UserId)
        {
            try
            {
                var AcaProfile = await _context.Users
                    .Where(a => a.Id == UserId)
                    .Include(a => a.Ward)
                    .ThenInclude(u => u.District)
                    .ThenInclude(u => u.Province)
                    .Where(u => u.WardId == u.Ward.Id)
                    .FirstOrDefaultAsync();
                return AcaProfile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PublicProfileUser(Guid userId)
        {
            try
            {
                var user = await _context.Users
                    .Where(u => u.Id == userId)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    if (user.PublicProfile == PublicProfile.INACTIVE)
                    {
                        user.PublicProfile = PublicProfile.ACTIVE;
                    } 
                    else if (user.PublicProfile == PublicProfile.ACTIVE)
                    {
                        user.PublicProfile = PublicProfile.INACTIVE;
                    }
                    
                }
                else
                {
                    { throw new Exception("User does not exist"); }
                    return false;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            try
            {
                var list = await _context.Users
                    .Include(x => x.Role)
                    .Include(u => u.University)
                    .Include(a => a.Ward)
                    .ThenInclude(u => u.District)
                    .ThenInclude(u => u.Province)
                    .Where(u => u.WardId == u.Ward.Id)
                    .ToListAsync();
                return list ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> BanUser(Guid UserId)
        {
            try
            {
                var user = await _context.Users
                    .Where(u => u.Id == UserId)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    if (user.Status == Status.ACTIVE)
                    {
                        user.Status = Status.BANNED;
                    } 
                    else if (user.Status == Status.BANNED)
                    {
                        user.Status = Status.ACTIVE;
                    }
                    
                }
                else
                {
                    { throw new Exception("User does not exist"); }
                }

                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> RoleUser(Guid UserId, string roleChange)
        {
            try
            {
                var user = await _context.Users
                    .Where(u => u.Id == UserId)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                   user.RoleId = roleChange;
                }
                else
                {
                    { throw new Exception("User does not exist"); }
                }

                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
