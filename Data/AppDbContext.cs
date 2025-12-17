using asp.net_core_web_api_Day_1.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace asp.net_core_web_api_Day_1.Data
{
    public class AppDbContext: IdentityDbContext<IdentityUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }    
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
                    
        }
    }
}
