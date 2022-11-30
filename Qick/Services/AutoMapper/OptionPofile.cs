using AutoMapper;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class OptionPofile : Profile
    {
        public OptionPofile()
        {
            CreateMap<Option, OptionResponse>();
            CreateMap<Option, OptionForAdminResponse>();
            CreateMap<UpdateQuestionRequest, OptionRequest>();
            CreateMap<UpdateOptionRequest, OptionRequest>();
            CreateMap<ListResponse<Option>, ListResponse<OptionResponse>>();
        }
    }
}
