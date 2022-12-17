using AutoMapper;
using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<Job, CareerJobResponse>();
            CreateMap<Job, JobResponse>();

            CreateMap<UpdateJobRequest,Job>();

            CreateMap<JobMajorRequest, JobMajor>()
                .ForMember(m => m.Status, n => n.MapFrom(i => Status.ACTIVE));
            

        }
    }
}
