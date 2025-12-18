using System.ComponentModel.DataAnnotations;

namespace asp.net_core_web_api_Day_1.Models.Entity
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
