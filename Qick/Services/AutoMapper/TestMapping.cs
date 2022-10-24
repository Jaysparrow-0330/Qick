using AutoMapper;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class TestMapping : Profile
    {
        public TestMapping()
        {
            CreateMap<Test, TakingTestResponse>()
                .ForMember(m => m.questions, n => n.MapFrom(i => i.Questions.ToList()));
            CreateMap<Test, ListTestForAdminResponse>()
                .ForMember(m => m.QuizTypeName, i => i.MapFrom(s => s.QuizType.QuizTypeName))
                .ForMember(m => m.UserName, i => i.MapFrom(s => s.Creator.UserName));
            CreateMap<Test, TakingTestResponse>()
                .ForMember(m => m.questions, n => n.MapFrom(i => i.Questions.ToList()));
            CreateMap<Test, ListTestResponse>();
            CreateMap<Test, TestDetailResponse>();
            CreateMap<Test, CreateTestResponseStepOne>();
            CreateMap<TestType, TestTypeResponse>();
        }
        
    }
}
