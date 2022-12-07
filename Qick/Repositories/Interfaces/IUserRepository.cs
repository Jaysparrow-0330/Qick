
using Qick.Dto.Requests;
using Qick.Models;


namespace Qick.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Login(LoginRequest user);
        Task<User> LoginAd(LoginRequest user);
        Task<User> LoginUni(LoginRequest user);
        Task<User> RegisterStaff(ManagerStaffRequest register, string code);
        Task<User> RegisterUni(ManagerStaffRequest register, string code);
        Task<User> Register(RegisterRequest register, string code);
        Task<bool> EmailExist(string email);
        Task<bool> EmailExistStaff(string email);
        Task<bool> EmailExistUniMa(string email);
        Task<User> GetUserByEmail(string email);
        Task<User> UpdatePassword(UpdatePassRequest update, User user);
        Task<User> GetUserById(Guid UserId);
        Task<User> ActiveUserStatus(User user, string code);
        // update user profile
        Task<User> UpdateProfile(UserProfileUpdateRequest request, Guid id);

        Task<AcademicProfile> GetAcademicProfile(Guid? UserId);

        Task<User> GetProfile(Guid UserId);

        Task<IEnumerable<User>> GetAllUser();
        Task<User> BanUser(Guid UserId);
        Task<bool> SaveUni(SaveUniRequest request, Guid userId);
        Task<User> RoleUser(Guid UserId,string roleChange);

        // create Acadamic Profile
        Task<AcademicProfile> CreateAcademicProfile(CreateAcademyRequest request,Guid userId);

        // update user profile
        Task<AcademicProfile> UpdateAcademicProfile(UpdateAcademyRequest request);
        // update user profile
        Task<bool> PublicProfileUser(Guid userId);
    }
}
