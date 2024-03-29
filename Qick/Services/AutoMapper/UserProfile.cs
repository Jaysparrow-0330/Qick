﻿using AutoMapper;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SavedUni, SaveUniResponse>()
                .ForMember(m => m.UniName, n => n.MapFrom(i => i.University.UniName))
                .ForMember(m => m.AvatarUrl, n => n.MapFrom(i => i.University.AvatarUrl));
            CreateMap<User, ProfileResponse>()
                .ForMember(m => m.HighSchoolName, n => n.MapFrom(i => i.HighSchool.HighSchoolName))
                .ForMember(m => m.WardId, n => n.MapFrom(i => i.Ward.Id))
                .ForMember(m => m.DistrictId, n => n.MapFrom(i => i.Ward.District.Id))
                .ForMember(m => m.ProvinceId, n => n.MapFrom(i => i.Ward.District.Province.Id));

            CreateMap<User, ListUserResponse>()
                .ForMember(m => m.HighSchoolName, n => n.MapFrom(i => i.HighSchool.HighSchoolName))
                .ForMember(m => m.UniName, n => n.MapFrom(i => i.University.UniName))
                .ForMember(m => m.RoleName, n => n.MapFrom(i => i.Role.RoleName))
                .ForMember(m => m.WardId, n => n.MapFrom(i => i.Ward.Id))
                .ForMember(m => m.DistrictId, n => n.MapFrom(i => i.Ward.District.Id))
                .ForMember(m => m.ProvinceId, n => n.MapFrom(i => i.Ward.District.Province.Id));

            CreateMap<User, ListCandidateResponse>()
                .ForMember(m => m.DistrictId, n => n.MapFrom(i => i.Ward.District.Id))
                .ForMember(m => m.ProvinceId, n => n.MapFrom(i => i.Ward.District.Province.Id));

            CreateMap<User, CandidateResponse>()
                .ForMember(m => m.UserId, n => n.MapFrom(i => i.Id))
                .ForMember(m => m.GraduationYear, n => n.MapFrom(i => i.AcademicProfiles.Where(x => x.UserId == i.Id).First().GraduationYear))
                .ForMember(m => m.AvarageScore, n => n.MapFrom(i => i.AcademicProfiles.Where(x => x.UserId == i.Id).First().AverageScore))
                .ForMember(m => m.AcademicRank, n => n.MapFrom(i => i.AcademicProfiles.Where(x => x.UserId == i.Id).First().AcademicRank))
                .ForMember(m => m.HighSchoolId, n => n.MapFrom(i => i.AcademicProfiles.Where(x => x.UserId == i.Id).First().HighSchool.Id))
                .ForMember(m => m.HighSchoolName, n => n.MapFrom(i => i.AcademicProfiles.Where(x => x.UserId == i.Id).First().HighSchool.HighSchoolName))
                .ForMember(m => m.HighSchoolCode, n => n.MapFrom(i => i.AcademicProfiles.Where(x => x.UserId == i.Id).First().HighSchool.HighSchoolCode))
                .ForMember(m => m.HighSchoolAddress, n => n.MapFrom(i => i.AcademicProfiles.Where(x => x.UserId == i.Id).First().HighSchool.HighSchoolAddress))
                .ForMember(m => m.DistrictId, n => n.MapFrom(i => i.Ward.District.Id))
                .ForMember(m => m.ProvinceId, n => n.MapFrom(i => i.Ward.District.Province.Id));

        }
    }
}
