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
        }
    }
}
