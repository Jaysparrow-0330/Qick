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

        public async Task<bool> CreateOption(Question question, CreateOptionRequest opt)
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

        public async Task<Option> GetOptionById(int optionId)
        {
            var optionDetail = await _context.Options
                .Where(a => a.Id == optionId)
                .FirstOrDefaultAsync();
            return optionDetail;
        }

        public async Task<Option> UpdateOptionInformation(UpdateOptionRequest option)
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
                }
                else
                {
                    { throw new Exception("Question does not exist"); }
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
