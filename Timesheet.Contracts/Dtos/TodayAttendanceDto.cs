using System;

namespace Timesheet.Contracts.Dtos
{
    public class TodayAttendanceDto
    {
        public string AccountId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Login { get; set; }
        public string Logout { get; set; }
    }
}
