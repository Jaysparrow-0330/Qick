using Microsoft.EntityFrameworkCore;
using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        private readonly QickDatabaseManangementContext _context;

        public OptionRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateOption(Question question, OptionRequest opt)
        {
            try
            {
                    Option addOpt = new()
                    {
                        QuestionId = question.Id,
                        OptionContent = opt.OptionContent,
                        OptionType = question.QuestionTypeId.ToString(),
                        CreatedDate = DateTime.Now,
                        Status = Status.ACTIVE,
                        Value = opt.Value
                    };
                await _context.Options.AddAsync(addOpt);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Option>> GetListOptionBasedOnQuestionId(int questionId)
        {
            try
            {
                var options = _context.Options
                    .Where(m => m.QuestionId == questionId && m.Status == Status.ACTIVE).ToListAsync();
                return await options;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<IEnumerable<Option>> GetListAllOptionBasedOnQuestionId(int questionId)
        {
            try
            {
                var options = _context.Options
                    .Where(m => m.QuestionId == questionId ).ToListAsync();
                return await options;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<Option> GetOptionById(int? optionId)
        {
            try
            {
                var optionDetail = await _context.Options
                .Where(a => a.Id == optionId)
                .FirstOrDefaultAsync();
                if (optionDetail != null)
                {
                    return optionDetail;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                  throw;
            }
        }

        public async Task<Option> UpdateOptionInformation(OptionRequest option)
        {
            try
            {
                var optionDb = await _context.Options
                    .Where(u => u.Id == option.Id)
                    .FirstOrDefaultAsync();

                if (optionDb != null)
                {
                    optionDb.OptionContent = option.OptionContent;
                    optionDb.Value = option.Value;
                    optionDb.Status = option.Status;
                }
                else
                {
                    { throw new Exception("Option does not exist"); }
                }

                await _context.SaveChangesAsync();
                return optionDb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
