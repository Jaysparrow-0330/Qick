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
                .ForMember(m => m.WardId, n => n.MapFrom(i => i.Ward.Id))
                .ForMember(m => m.DistrictId, n => n.MapFrom(i => i.Ward.District.Id))
                .ForMember(m => m.ProvinceId, n => n.MapFrom(i => i.Ward.District.Province.Id));
        }
    }
}
