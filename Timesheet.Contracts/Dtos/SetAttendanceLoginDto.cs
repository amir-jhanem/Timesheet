using System;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.Contracts.Dtos
{
    public class SetAttendanceLoginDto
    {
        [Required]
        public string AccountId { get; set; }
        [Required]
        public DateTime LoginDate { get; set; }
        [Required]
        public string LoginTime { get; set; }
    }
}
