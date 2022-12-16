using Qick.Dto.Requests;
using Qick.Models;
namespace Qick.Repositories.Interfaces
{
    public interface IFQARepository
    {
        Task<bool> CreateFQA(CreateFQARequest request,Guid UniId,Guid UserId);

        Task<Fqa> UpdateFQA(UpdateFQARequest request); 

        Task<Fqa> DeleteFQA(int FQAId);

        Task<IEnumerable<Fqa>> GetListUniFQA(Guid? UniId);
        
        Task<Fqa> FQADetail(int FQAId);

        //FQA Topic methods
        Task<IEnumerable<Fqatopic>> ListFQATopic();

        Task<bool> CreateFQATopic(CreateFQATopicRequest request);

        Task<Fqatopic> UpdateFQATopic(UpdateFQATopicRequest request);

        Task<Fqatopic> DeleteFQATopic(int FQATopicId);

        Task<IEnumerable<Fqatopic>> GetUniFQAById(Guid? UniId);
    }
}
