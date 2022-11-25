
using Qick.Dto.Requests;
using Qick.Models;


namespace Qick.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Login(LoginRequest user);
        Task<User> LoginAd(LoginRequest user);
        Task<User> LoginUni(LoginRequest user);
        Task<User> Register(RegisterRequest register);
        Task<bool> EmailExist(string email);

        // update user profile
        Task<User> UpdateProfile(UserProfileUpdateRequest request, Guid id);

        Task<AcademicProfile> GetAcademicProfile(Guid? UserId);

        Task<User> GetProfile(Guid UserId);

        Task<IEnumerable<User>> GetUser();
        Task<User> BanUser(Guid UserId);

        // create Acadamic Profile
        Task<AcademicProfile> CreateAcademicProfile(CreateAcademyRequest request,Guid userId);

        // update user profile
        Task<AcademicProfile> UpdateAcademicProfile(UpdateAcademyRequest request);
        // update user profile
        Task<bool> PublicProfileUser(Guid userId);
    }
}
