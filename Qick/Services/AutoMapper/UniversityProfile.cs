using AutoMapper;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class UniversityProfile : Profile
    {
        public UniversityProfile()
        {
            CreateMap<University, ListUniResponse>();
            CreateMap<University, UniDetailResponse>()
            .ForMember(m => m.WardId, n => n.MapFrom(i => i.Ward.Id))
            .ForMember(m => m.DistrictId, n => n.MapFrom(i => i.Ward.District.Id))
            .ForMember(m => m.ProvinceId, n => n.MapFrom(i => i.Ward.District.Province.Id));
        }
    }
}
