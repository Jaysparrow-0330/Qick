
using Microsoft.EntityFrameworkCore;
using Qick.Controllers.Requests;
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
        public async Task<User> Login(LoginRequest userIn)
        {
            var user = await _context.Users.Where(u => u.Email == userIn.Email.ToLower() && u.Status == "ACTIVE" && u.RoleId == "ADMIN").SingleOrDefaultAsync();
            if (user == null)
                return null;

            if (!VerifyPasswordHash(userIn.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;


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
    }
}
