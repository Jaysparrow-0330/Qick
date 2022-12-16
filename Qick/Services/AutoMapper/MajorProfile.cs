using AutoMapper;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class MajorProfile : Profile
    {
        public MajorProfile()
        {
            CreateMap<Major, MajorResponse>();
            CreateMap<Major, CareerMajorResponse>();
            CreateMap<Major, ListMajorUniResponse>()
                .ForMember(m => m.spec, n => n.MapFrom(i => i.Specializations.ToList()));
            CreateMap<Specialization, ListUniSpecResponse>()
                .ForMember(m => m.Id, n => n.MapFrom(i => i.UniversitySpecializations.Where(x => x.SpecId == i.Id).FirstOrDefault().Id))
                .ForMember(m => m.UniId, n => n.MapFrom(i => i.UniversitySpecializations.Where(x => x.SpecId == i.Id).FirstOrDefault().UniId))
                .ForMember(m => m.SpecId, n => n.MapFrom(i => i.Id))
                .ForMember(m => m.UniSpecName, n => n.MapFrom(i => i.UniversitySpecializations.Where(x => x.SpecId == i.Id).FirstOrDefault().UniSpecName));
            
            CreateMap<Specialization, ListSpecDbResponse>();
        }
    }
}
