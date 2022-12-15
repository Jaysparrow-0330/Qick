using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly QickDatabaseManangementContext _context;
        public NewsRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateNews(CreateNewsRequest request, Guid UniId, Guid UserId)
        {
            try
            {
                AddmissionNews addNews = new()
                {
                    Title = request.Title,
                    Content = request.Content,
                    BannerUrl = request.BannerUrl,
                    UniSpecId = request.UniSpecId,
                    UniId = UniId,
                    UserId = UserId,
                    CreateDate = DateTime.Now,
                    Status = Status.ACTIVE
                };
                await _context.AddmissionNews.AddAsync(addNews);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
