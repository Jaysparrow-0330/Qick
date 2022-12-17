using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class FaqProfile : Profile
    {
        public FaqProfile()
        {
            CreateMap<Fqatopic, ListFqaTopicResponse>();

            CreateMap<Fqa,FQAResponse>()
                .ForMember(m=>m.TopicName,n => n.MapFrom(i => i.Topic.TopicName));

            CreateMap<Fqatopic, ListFqaResponse>();

        }
    }
}
