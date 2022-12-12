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

    }
}
