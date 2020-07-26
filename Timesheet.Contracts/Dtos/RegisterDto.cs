using System.ComponentModel.DataAnnotations;

namespace Timesheet.Contracts.Dtos
{
    public class RegisterDto
    {
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
    }
}
