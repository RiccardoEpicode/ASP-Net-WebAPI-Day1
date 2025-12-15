using System.ComponentModel.DataAnnotations;

namespace asp.net_core_web_api_Day_1.Models
{
    public class StudentDto
    {
        [Required]
        public string Nome { get; set; } = null!;

        [Required]
        public string Cognome { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
