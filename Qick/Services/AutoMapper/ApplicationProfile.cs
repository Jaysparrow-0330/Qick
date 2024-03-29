﻿using AutoMapper;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<HighSchool, ListHighSchoolResponse>()
            .ForMember(m => m.WardId, n => n.MapFrom(i => i.Ward.Id))
            .ForMember(m => m.DistrictId, n => n.MapFrom(i => i.Ward.District.Id))
            .ForMember(m => m.ProvinceId, n => n.MapFrom(i => i.Ward.District.Province.Id));

            CreateMap<Application, ListApplicationResponse>()
            .ForMember(m => m.AddressNumber, n => n.MapFrom(i => i.User.AddressNumber))
            .ForMember(m => m.AvatarUrl, n => n.MapFrom(i => i.User.AvatarUrl))
            .ForMember(m => m.Age, n => n.MapFrom(i => i.User.Age))
            .ForMember(m => m.Email, n => n.MapFrom(i => i.User.Email))
            .ForMember(m => m.Gender, n => n.MapFrom(i => i.User.Gender))
            .ForMember(m => m.Phone, n => n.MapFrom(i => i.User.Phone))
            .ForMember(m => m.UserName, n => n.MapFrom(i => i.User.UserName))
            .ForMember(m => m.UniSpecName, n => n.MapFrom(i => i.UniSpec.UniSpecName))
            .ForMember(m => m.UniName, n => n.MapFrom(i => i.Uni.UniName));

            CreateMap<Application, ApplicationDetailResponse>()
            .ForMember(m => m.AddressNumber, n => n.MapFrom(i => i.User.AddressNumber))
            .ForMember(m => m.AvatarUrl, n => n.MapFrom(i => i.User.AvatarUrl))
            .ForMember(m => m.Age, n => n.MapFrom(i => i.User.Age))
            .ForMember(m => m.Email, n => n.MapFrom(i => i.User.Email))
            .ForMember(m => m.Gender, n => n.MapFrom(i => i.User.Gender))
            .ForMember(m => m.Phone, n => n.MapFrom(i => i.User.Phone))
            .ForMember(m => m.UserName, n => n.MapFrom(i => i.User.UserName))
            .ForMember(m => m.UniSpecName, n => n.MapFrom(i => i.UniSpec.UniSpecName))
            .ForMember(m => m.SpecCode, n => n.MapFrom(i => i.UniSpec.SpecCode))
            .ForMember(m => m.DateOfBirth, n => n.MapFrom(i => i.User.DateOfBirth))
            .ForMember(m => m.CredentialId, n => n.MapFrom(i => i.User.CredentialId))
            .ForMember(m => m.CredentialFrontImgUrl, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().CredentialFrontImgUrl))
            .ForMember(m => m.CredentialBackImgUrl, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().CredentialBackImgUrl))
            .ForMember(m => m.HighSchoolCode, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().HighSchool.HighSchoolCode))
            .ForMember(m => m.HighSchoolName, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().HighSchool.HighSchoolName))
            .ForMember(m => m.HighSchoolAddress, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().HighSchool.HighSchoolAddress))
            .ForMember(m => m.GraduationYear, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().GraduationYear))
            .ForMember(m => m.AvarageScore, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().AverageScore))
            .ForMember(m => m.AcademicRank, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().AcademicRank))
            .ForMember(m => m.HighSchoolId, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().HighSchoolId))
            .ForMember(m => m.HighSchoolName, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().HighSchool.HighSchoolName))
            .ForMember(m => m.HighSchoolCode, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().HighSchool.HighSchoolCode))
            .ForMember(m => m.HighSchoolAddress, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().HighSchool.HighSchoolAddress));
        }
    }
}
