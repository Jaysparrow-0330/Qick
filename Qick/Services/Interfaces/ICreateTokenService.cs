using Qick.Models;

namespace Qick.Services.Interfaces
{
    public interface ICreateTokenService
    {
        string CreateToken(User user, bool isUpdate, DateTime? updateDay);

        string CreateTokenTest(User user, double min);
    }
}
