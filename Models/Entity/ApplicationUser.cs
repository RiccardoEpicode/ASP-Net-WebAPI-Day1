using Microsoft.AspNetCore.Identity;

namespace asp.net_core_web_api_Day_1.Models.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
