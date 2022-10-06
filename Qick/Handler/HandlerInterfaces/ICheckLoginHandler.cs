using Qick.Controllers.Requests;
using Qick.Controllers.Responses;

namespace Qick.Handler.HandlerInterfaces
{
    public interface ICheckLoginHandler
    {
        public Task<LoginResponse> Handle(LoginRequest request);
    }
}
