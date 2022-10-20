﻿using AutoMapper;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class QuestionMapping : Profile
    {
        public QuestionMapping()
        {
            CreateMap<Question, QuestionResponse>()
            .ForMember(m => m.Options, n => n.MapFrom(i => i.Options.ToList()));
            CreateMap<ListResponse<Question>, ListResponse<QuestionResponse>>();
        }
    }
}
