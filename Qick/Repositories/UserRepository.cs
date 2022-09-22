using Qick.Model.Data;
using Qick.Model.Input;
using Qick.Model.Output;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        ///  Login authentication by email and password 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<LoginOutput> Login(LoginInput userIn)
        {
            LoginOutput userOut = null;
            if (userIn.Email == "email@email.com" && userIn.Password == "password") {
                 userOut = new()
                {

                    UserName = "Name",
                    Email = "Mail"

                };
            } 

             return userOut;
        }

      
    }
}
