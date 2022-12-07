using Qick.Models;

namespace Qick.Services.Interfaces
{
    public interface ICreateTokenService
    {
        string CreateToken(User user);
        string CreateTokenForUniFirstLogin(User user);
        string CreateTokenTest(User user, double min);
    }
}
