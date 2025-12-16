using System.ComponentModel.DataAnnotations;

namespace asp.net_core_web_api_Day_1.Models
{
    public class StudentProfileDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(16, MinimumLength = 16)]
        public string FiscalCode { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int StudentId { get; set; }
    }
}
