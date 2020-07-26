using System;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.Contracts.Dtos
{
    public class SetAttendanceLogoutDto
    {
        [Required]
        public string AccountId { get; set; }
        [Required]
        public DateTime LogoutDate { get; set; }
        [Required]
        public string LogoutTime { get; set; }
    }
}
