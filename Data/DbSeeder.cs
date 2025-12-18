using asp.net_core_web_api_Day_1.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace asp.net_core_web_api_Day_1.Data
{
    public static class DbSeeder
    {
        public static async Task SeedUsersAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            // USER SEED
            if (await userManager.FindByNameAsync("admin") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@test.com",
                    FirstName = "Admin",
                    LastName = "User"
                };

                await userManager.CreateAsync(user, "Password123!");
            }
        }
    }
}
