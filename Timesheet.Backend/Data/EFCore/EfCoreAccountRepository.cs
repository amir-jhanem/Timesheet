using Timesheet.Backend.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Timesheet.Backend.Data.EFCore
{
    public class EfCoreAccountRepository : EfCoreRepository<Account, TimesheetDbContext>
    {
        private readonly TimesheetDbContext Context;
        public EfCoreAccountRepository(TimesheetDbContext context) : base(context)
        {
            Context = context;
        }

        public async Task<Account> Get(string email, string password)
        {
            return await Context.Set<Account>().SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
    }
}
