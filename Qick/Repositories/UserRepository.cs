
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
        /// <summary>
        ///  Login authentication by email and password 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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
                var user = await _context.Users.Where(u => u.Email.Equals(login.Email.ToLower()) && u.RoleId.Equals(Roles.ADMIN) || u.RoleId.Equals(Roles.GOD) && u.Status != Status.DISABLE).FirstOrDefaultAsync();
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
    }
}
