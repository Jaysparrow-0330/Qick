﻿using AutoMapper;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class MailProfile : Profile
    {
        public MailProfile()
        {
            CreateMap<MailBox, MailBoxResponse>()
                .ForMember(m => m.UniName, n => n.MapFrom(i => i.Uni.UniName))
                .ForMember(m => m.UserName, n => n.MapFrom(i => i.User.UserName))
                .ForMember(m => m.UniAvatarUrl, n => n.MapFrom(i => i.Uni.AvatarUrl))
                .ForMember(m => m.UserAvatarUrl, n => n.MapFrom(i => i.User.AvatarUrl));
        }
    }
}
