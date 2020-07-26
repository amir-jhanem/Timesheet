using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Timesheet.Backend.Data;

namespace Timesheet.Backend.Models
{
    public class Account : IEntity
    {
        public string Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(245)]
        public string Email { get; set; }
        [Required]
        [StringLength(32)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string MobileNumber { get; set; }

        public ICollection<AttendanceSheet> AttendanceSheets { get; set; }
    }
}
