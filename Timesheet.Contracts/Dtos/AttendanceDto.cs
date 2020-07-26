using System;

namespace Timesheet.Contracts.Dtos
{
    public class AttendanceDto
    {
        public string AccountId { get; set; }
        public DateTime Date { get; set; }
        public string Login { get; set; }
        public string Logout { get; set; }
    }
}
