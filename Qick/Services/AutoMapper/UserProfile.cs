using AutoMapper;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ProfileResponse>()
                .ForMember(m => m.HighSchoolName, n => n.MapFrom(i => i.HighSchool.HighSchoolName))
                .ForMember(m => m.WardName, n => n.MapFrom(i => i.Ward.WardName))
                .ForMember(m => m.DistrictName, n => n.MapFrom(i => i.Ward.District.DistrictName))
                .ForMember(m => m.ProvinceName, n => n.MapFrom(i => i.Ward.District.Province.ProvinceName));
        }
    }
}
