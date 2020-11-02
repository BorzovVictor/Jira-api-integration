using System.Threading.Tasks;
using Jira.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Jira.Api.Infrastructure
{
    public interface IDatabaseInitializer
    {
        Task Seed();
    }
    
    public class DatabaseInitializer: IDatabaseInitializer
    {
        private DataContext _context;
        private HangFireContext _hangFireContext;

        public DatabaseInitializer(DataContext context, HangFireContext hangFireContext)
        {
            _context = context;
            _hangFireContext = hangFireContext;
        }

        public async Task Seed()
        {
            await _hangFireContext.Database.MigrateAsync().ConfigureAwait(false);
            await _context.Database.MigrateAsync().ConfigureAwait(false);
        }
    }
    
}