using Qick.Models;
using Qick.Dto.Requests;

namespace Qick.Repositories.Interfaces
{
    public interface INewsRepository
    {
        Task<bool> CreateNews(CreateNewsRequest request, Guid UniId, Guid UserId);

        Task<AddmissionNews> ApproveNews(int newsId,string status);

        Task<AddmissionNews> UpdateNews(UpdateNewsRequest request);

        Task<AddmissionNews> GetNewsById(int newsId);

        Task<bool> DeleteNews(int newsId);
    }
}
