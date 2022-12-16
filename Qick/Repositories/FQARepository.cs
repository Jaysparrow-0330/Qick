using Microsoft.EntityFrameworkCore;
using Qick.Dto.Requests;
using Qick.Models;
using Qick.Dto;
using Qick.Repositories.Interfaces;
using Qick.Dto.Enum;

namespace Qick.Repositories
{
    public class FQARepository : IFQARepository
    {
        private readonly QickDatabaseManangementContext _context;
        public FQARepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateFQA(CreateFQARequest request,Guid UniId,Guid UserId)
        {
            try
            {
                Fqa FQA = new()
                {
                    UniId = UniId,
                    UserId = UserId,
                    CreatedDate = DateTime.Now,
                    Fqacontent = request.Fqacontent,
                    Fqaanswer = request.Fqaanswer,
                    Status = Status.ACTIVE,
                    TopicId = request.TopicId,
                };
                await _context.Fqas.AddAsync(FQA);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateFQATopic(CreateFQATopicRequest request)
        {
            try
            {
                Fqatopic topic = new()
                {
                    TopicName = request.TopicName,
                    Status = request.Status
                };
                await _context.Fqatopics.AddAsync(topic);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Fqa> DeleteFQA(int FQAId)
        {
            try
            {
                var response = _context.Fqas
                    .Where(i => i.Id == FQAId)
                    .FirstOrDefault();
                if (response != null)
                {
                    response.Status = Status.DISABLE;
                }
                else
                {
                    { throw new Exception("FQA does not exist"); }
                }
                await _context.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Fqatopic> DeleteFQATopic(int FQATopicId)
        {
            try
            {
                var response = _context.Fqatopics
                    .Where(i => i.Id == FQATopicId)
                    .FirstOrDefault();
                if (response != null)
                {
                    response.Status = Status.DISABLE;
                }
                else
                {
                    { throw new Exception("FQA topic does not exist"); }
                }
                await _context.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Fqa> FQADetail(int FQAId)
        {
            try
            {
                var response = _context.Fqas
                    .Where(i => i.Id == FQAId)
                    .FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Fqatopic>> ListFQATopic()
        {
            try
            {
                var response = await _context.Fqatopics
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Fqa>> GetListUniFQA(Guid? UniId)
        {
            try
            {
                var response = await _context.Fqas
                    .Include(i => i.Topic)
                    .Where(i => i.UniId == UniId)
                    .Where(i => i.Status == Status.ACTIVE)
                    .OrderByDescending(i => i.TopicId)
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Fqa> UpdateFQA(UpdateFQARequest request)
        {
            try
            {
                var response = _context.Fqas
                    .Where(i => i.Id == request.Id)
                    .FirstOrDefault();

                if(response != null)
                {
                    response.Fqaanswer = request.Fqaanswer;
                    response.Status = request.Status;
                    response.Fqacontent = request.Fqacontent;
                    response.TopicId= request.TopicId;  
                }
                else
                {
                    { throw new Exception("FQA does not exist"); }
                }
                await _context.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Fqatopic> UpdateFQATopic(UpdateFQATopicRequest request)
        {
            try
            {
                var response = _context.Fqatopics
                    .Where(i => i.Id == request.Id)
                    .FirstOrDefault();
                if (response != null)
                {
                    response.TopicName = request.TopicName;
                    response.Status= request.Status;
                }
                else
                {
                    { throw new Exception("FQA topic does not exist"); }
                }
                await _context.SaveChangesAsync();
                return response;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Fqatopic>> GetUniFQAById(Guid? UniId)
        {
            try
            {
                var response = await _context.Fqatopics
                    .Where(i => i.Status == Status.ACTIVE)
                    .Where(i => i.Fqas
                    .Where(x => x.TopicId == i.Id)
                    .Where(x => x.UniId == UniId)
                    .Count() > 0)
                    .Include(i => i.Fqas.Where(x => x.Status == Status.ACTIVE).Where(x => x.UniId == UniId))
                    .ToListAsync();
                if (response != null)
                {
                    return response;
                }
                else
                {
                    { throw new Exception("Fqas list not found"); }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
