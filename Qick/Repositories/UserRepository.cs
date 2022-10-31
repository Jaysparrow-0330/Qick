
using Microsoft.EntityFrameworkCore;
using Qick.Dto.Requests;
using Qick.Dto.Enum;
using Qick.Models;
using Qick.Repositories.Interfaces;
using System.Security.Cryptography;
using System.Text;

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
                var user = await _context.Users.Where(u => u.Email.Equals(login.Email.ToLower()) && u.RoleId.Equals(Roles.MEMBER) && u.Status != Status.DISABLE).FirstOrDefaultAsync();
                if (user != null)
                {
                    if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                        return null;
                    else
                    {
                        return user;
                        
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
                if (await _context.Users.Where(a => a.RoleId != Roles.USER_GOOGLE).AnyAsync(x => x.Email.Equals(email.ToLower())))
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


        public async Task<User> Register(RegisterRequest register)
        {
            try
            {
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
                    Status = Status.ACTIVE
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
                var user = await _context.Users.Where(u => u.Email.Equals(login.Email.ToLower()) && u.RoleId.Equals(Roles.ADMIN) || u.RoleId.Equals(Roles.GOD) || u.RoleId.Equals(Roles.MANAGER) || u.RoleId.Equals(Roles.STAFF) && u.Status != Status.DISABLE).FirstOrDefaultAsync();
                if (user != null)
                {
                    if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                    {
                        return null;
                    } 
                    else
                    {
                        return user;
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
                var user = await _context.Users.Where(u => u.Email.Equals(login.Email.ToLower()) && u.RoleId.Equals(Roles.MANAGER) || u.RoleId.Equals(Roles.STAFF) && u.Status != Status.DISABLE).FirstOrDefaultAsync();
                if (user != null)
                {
                    if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                    {
                        return null;
                    }
                    else
                    {
                        return user;
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

        public async Task<User> UpdateProfile(UserProfileUpdateRequest request)
        {
            try
            {
                var user = await _context.Users
                    .Where(u => u.Id == request.Id)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    user.UserName = request.UserName;
                    user.Age = request.Age;
                    user.AddressNumber = request.AddressNumber;
                    user.AvatarUrl = request.AvatarUrl;
                    user.CredentialBackImgUrl = request.CredentialBackImgUrl;
                    user.CredentialFrontImgUrl = request.CredentialFrontImgUrl;
                    user.CredentialId = request.CredentialId;
                    user.DateOfBirth = request.DateOfBirth;
                    user.Gender = request.Gender;
                    user.Phone = request.Phone;
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

        public Task<AcademicProfile> CreateAcademicProfile(CreateAcademyRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AcademicProfile> UpdateAcademicProfile(UpdateAcademyRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AcademicProfile> GetAcademicProfile(Guid UserId)
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
    }
}
