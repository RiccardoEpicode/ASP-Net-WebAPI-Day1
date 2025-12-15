using System.ComponentModel.DataAnnotations;

namespace asp.net_core_web_api_Day_1.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Cognome { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
