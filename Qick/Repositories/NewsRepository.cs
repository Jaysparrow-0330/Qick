using Microsoft.EntityFrameworkCore;
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

        public async Task<AddmissionNews> ApproveNews(int newsId,string status)
        {
            try
            {
                var newsDb = await _context.AddmissionNews
                .Where(u => u.Id == newsId)
                .FirstOrDefaultAsync();
                if (newsDb != null)
                {
                    newsDb.Status = status;
                }
                else
                {
                    { throw new Exception("News does not exist"); }
                }
                await _context.SaveChangesAsync();
                return newsDb;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
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
                    Status = Status.PENDING
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

        public async Task<AddmissionNews> GetNewsById(int newsId)
        {
            try
            {
                var newsDb = await _context.AddmissionNews
                    .Where(u => u.Id == newsId)
                    .FirstOrDefaultAsync();
                if(newsDb != null)
                {
                    return newsDb;
                }
                else
                {
                    { throw new Exception("News does not exist"); }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteNews(int newsId)
        {
            try
            {
                var newsDb = await _context.AddmissionNews
                   .Where(u => u.Id == newsId)
                   .FirstOrDefaultAsync();
                if(newsDb != null) {
                    newsDb.Status = Status.DISABLE;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AddmissionNews> UpdateNews(UpdateNewsRequest request)
        {
            try
            {
                var news = await _context.AddmissionNews
                  .Where(u => u.Id == request.Id)
                  .FirstOrDefaultAsync();
                if (news != null)
                {
                    news.Title = request.Title;
                    news.Content = request.Content;
                    news.UniSpecId= request.UniSpecId;
                    news.BannerUrl = request.BannerUrl;
                    news.Status= request.Status;
                }
                else
                {
                    throw new Exception("News does not exist"); 
                }

                await _context.SaveChangesAsync();
                return news;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<AddmissionNews>> GetAllNews()
        {
            try
            {
                var news = await _context.AddmissionNews.Where(x => x.Status == Status.ACTIVE).OrderByDescending(x => x.CreateDate).ToListAsync();
                return news;
            }
            catch (Exception ex )
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<AddmissionNews>> GetNewsByUniId(Guid? UniId)
        {
            try
            {
                var news = await _context.AddmissionNews
                    .Where(x => x.UniId == UniId)
                    .Include(x => x.Uni).ThenInclude(i => i.UniversitySpecializations)
                    .OrderByDescending(x => x.CreateDate)
                    .ToListAsync();
                return news;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
