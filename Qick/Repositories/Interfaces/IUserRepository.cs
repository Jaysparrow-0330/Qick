using Qick.Model.Data;
using Qick.Model.Input;
using Qick.Model.Output;

namespace Qick.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param ></param>
        /// <returns>LoginOuput</returns>
        Task<LoginOutput> Login(LoginInput user);
    }
}
