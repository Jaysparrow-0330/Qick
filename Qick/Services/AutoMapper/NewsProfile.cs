using AutoMapper;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<AddmissionNews, ListNewsResponse>()
                .ForMember(m => m.UniSpecName, n => n.MapFrom(i => i.Uni.UniversitySpecializations.Where(x => x.Id == i.UniSpecId).Select(o => o.UniSpecName).FirstOrDefault()));
        }
    }
}
