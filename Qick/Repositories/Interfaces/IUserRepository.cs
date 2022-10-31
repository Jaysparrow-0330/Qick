﻿
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
        Task<User> UpdateProfile(UserProfileUpdateRequest request);

        Task<AcademicProfile> GetAcademicProfile(Guid UserId);

        // create Acadamic Profile
        Task<AcademicProfile> CreateAcademicProfile(CreateAcademyRequest request);

        // update user profile
        Task<AcademicProfile> UpdateAcademicProfile(UpdateAcademyRequest request);
    }
}
