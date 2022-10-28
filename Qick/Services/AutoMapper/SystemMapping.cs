using AutoMapper;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class SystemMapping : Profile
    {
        public SystemMapping()
        {
            CreateMap<Job,JobResponse>();
        }
    }
}
