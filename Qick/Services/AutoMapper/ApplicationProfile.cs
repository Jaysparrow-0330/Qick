using AutoMapper;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Application, ListApplicationResponse>()
            .ForMember(m => m.AddressNumber, n => n.MapFrom(i => i.User.AddressNumber))
            .ForMember(m => m.AvatarUrl, n => n.MapFrom(i => i.User.AvatarUrl))
            .ForMember(m => m.Age, n => n.MapFrom(i => i.User.Age))
            .ForMember(m => m.Email, n => n.MapFrom(i => i.User.Email))
            .ForMember(m => m.Gender, n => n.MapFrom(i => i.User.Gender))
            .ForMember(m => m.Phone, n => n.MapFrom(i => i.User.Phone))
            .ForMember(m => m.UserName, n => n.MapFrom(i => i.User.UserName))
            .ForMember(m => m.UniSpecName, n => n.MapFrom(i => i.UniSpec.UniSpecName));

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
            .ForMember(m => m.HighSchoolCode, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().HighSchoolCode))
            .ForMember(m => m.HighSchoolName, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().HighSchoolName))
            .ForMember(m => m.HighSchoolAddress, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().HighSchoolAddress))
            .ForMember(m => m.GraduationYear, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().GraduationYear))
            .ForMember(m => m.AvarageScore, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().AvarageScore))
            .ForMember(m => m.AcademicRank, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().AcademicRank))
            .ForMember(m => m.SchoolReport1Url, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().SchoolReport1Url))
            .ForMember(m => m.SchoolReport2Url, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().SchoolReport2Url))
            .ForMember(m => m.SchoolReport3Url, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().SchoolReport3Url))
            .ForMember(m => m.SchoolReport4Url, n => n.MapFrom(i => i.ApplicationDetails.Where(x => x.ApplicationId == i.Id).FirstOrDefault().SchoolReport4Url));
        }
    }
}
