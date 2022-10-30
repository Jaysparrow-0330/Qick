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
            CreateMap<Specialization, ListSpecDbResponse>();
        }
    }
}
