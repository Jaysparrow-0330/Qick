
using Qick.Controllers.Requests;
using Qick.Models;


namespace Qick.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param ></param>
        /// <returns>LoginOuput</returns>
        Task<User> Login(LoginRequest user);
        Task<User> Register(RegisterRequest register);
    }
}
