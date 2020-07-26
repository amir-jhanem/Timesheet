using System;

namespace Timesheet.GUI.Models
{
    public class AttendanceSheet 
    {
        public DateTime Date { get; set; }
        public string Login { get; set; }
        public string Logout { get; set; }
    }
}
