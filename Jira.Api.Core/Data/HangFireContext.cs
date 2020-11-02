using Microsoft.EntityFrameworkCore;

namespace Jira.Core.Data
{
    public class HangFireContext: DbContext
    {
        public HangFireContext(DbContextOptions<HangFireContext> options) :base(options)
        {
        }
    }
}