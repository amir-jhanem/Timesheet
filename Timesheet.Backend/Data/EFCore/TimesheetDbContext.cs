using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Linq;
using Timesheet.Backend.Models;

namespace Timesheet.Backend.Data.EFCore
{
    public class TimesheetDbContext: DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AttendanceSheet> AttendanceSheets { get; set; }

        public TimesheetDbContext(DbContextOptions<TimesheetDbContext> options) : base(options) 
        {
           
        }
    }
}
