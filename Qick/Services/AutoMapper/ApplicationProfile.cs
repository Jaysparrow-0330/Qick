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
        }
    }
}
