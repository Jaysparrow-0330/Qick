using AutoMapper;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class OptionMapping : Profile
    {
        public OptionMapping()
        {
            CreateMap<Option, OptionResponse>();
            CreateMap<Option, OptionForAdminResponse>();
            CreateMap<ListResponse<Option>, ListResponse<OptionResponse>>();
        }
    }
}
