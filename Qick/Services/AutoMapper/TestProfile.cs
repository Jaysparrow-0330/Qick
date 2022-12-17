using AutoMapper;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;

namespace Qick.Services.AutoMapper
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<Test, TakingTestResponse>()
                .ForMember(m => m.questions, n => n.MapFrom(i => i.Questions.ToList()));
            CreateMap<Test, ListTestForAdminResponse>()
                .ForMember(m => m.TestTypeName, i => i.MapFrom(s => s.TestType.TestTypeName))
                .ForMember(m => m.UserName, i => i.MapFrom(s => s.Creator.UserName));
            CreateMap<Test, TakingTestResponse>()
                .ForMember(m => m.questions, n => n.MapFrom(i => i.Questions.ToList()));
            CreateMap<Test, ListTestResponse>();
            CreateMap<Test, TestDetailResponse>();
            CreateMap<Test, CreateTestResponseStepOne>();
            CreateMap<TestType, TestTypeResponse>();
            CreateMap<Character, SubmitResponse>();
            CreateMap<Character, ResultResponse>();
            CreateMap<Attempt, ListAttemptResponse>()
                .ForMember(m => m.testName, n => n.MapFrom(i => i.Test.TestName))
                .ForMember(m => m.ResultName, n => n.MapFrom(i => i.Test.Characters.Where(u => u.ResultShortName.Equals(i.ResultShortName)).Select(a => a.ResultName).FirstOrDefault()));
        

            CreateMap<UpdateCharacterRequest, Character>();

        }
        
    }
}
