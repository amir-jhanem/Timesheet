using System;
using System.ComponentModel.DataAnnotations;
using Timesheet.Backend.Data;

namespace Timesheet.Backend.Models
{
    public class AttendanceSheet : IEntity
    {
        public string Id { get; set; }
        [Required]
        public string AccountId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public TimeSpan? Login { get; set; }
        public TimeSpan? Logout { get; set; }

        public Account Account { get; set; }
    }
}
