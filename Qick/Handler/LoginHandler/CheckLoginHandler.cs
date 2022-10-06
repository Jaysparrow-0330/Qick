using Qick.Controllers.Requests;
using Qick.Controllers.Responses;
using Qick.Handler.HandlerInterfaces;
using Qick.Repositories.Interfaces;
using Qick.Services.Interfaces;

namespace Qick.Handler.LoginHandler
{
    public class CheckLoginHandler : ICheckLoginHandler
    {
        private readonly IUserRepository _repo;
        private readonly ICreateTokenService _token;

        public CheckLoginHandler(IUserRepository repo, ICreateTokenService token)
        {
            _repo = repo;
            _token = token;
            
        }

        public async Task<LoginResponse> Handle(LoginRequest request)
        {
            try
            {
                var user = await _repo.Login(request);
                if (user == null) return null;
                //var check = await _uow.UserBans.CheckBan(user.Id);
                //if (check != null)
                //{
                //    if (check.ExpiredDate != null)
                //    {
                //        throw new BanException("Khoá tới ngày " + check.ExpiredDate.Value.Date.ToString() + " vì: " + check.Reason);
                //    }
                //    else
                //    {
                //        throw new BanException("Khoá vĩnh viễn vì: " + check.Reason);
                //    }
                //}
                //if (request.Login.DeviceId != null)
                //{
                //    var device = await _uow.UserDevices.CheckExist(user.Id, request.Login.DeviceId);
                //    if (device == null)
                //    {
                //        UserDevice deviceA = new()
                //        {
                //            Id = Guid.NewGuid(),
                //            UserId = user.Id,
                //            DeviceId = request.Login.DeviceId,
                //            Status = Status.ACTIVE
                //        };
                //        await _uow.UserDevices.Add(deviceA);
                //        if (await _uow.Complete() <= 0) { throw new Exception("Can't"); }
                //    }
                //}
                  return new LoginResponse() { Token = _token.CreateToken(user) };
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
