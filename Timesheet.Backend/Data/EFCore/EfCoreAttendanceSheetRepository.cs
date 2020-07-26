using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Timesheet.Backend.Models;

namespace Timesheet.Backend.Data.EFCore
{
    public class EfCoreAttendanceSheetRepository : EfCoreRepository<AttendanceSheet, TimesheetDbContext>
    {
        private readonly TimesheetDbContext Context;
        public EfCoreAttendanceSheetRepository(TimesheetDbContext context) : base(context)
        {
            Context = context;
        }

        public async Task<AttendanceSheet> GetTodayAccountAttendance(string accountId)
        {
            return await Context.Set<AttendanceSheet>().SingleOrDefaultAsync(x => x.AccountId == accountId && x.Date.Date == DateTime.Now.Date);
        }
        public async Task<AttendanceSheet> GetAccountAttendance(string accountId, DateTime date)
        {
            return await Context.Set<AttendanceSheet>().SingleOrDefaultAsync(x => x.AccountId == accountId && x.Date.Date == date.Date);
        }
    }
}
