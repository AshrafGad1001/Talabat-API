using Microsoft.AspNetCore.Identity;
using Talabat.core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Ashraf gad",
                    Email = "ashrafgad542@gmail.com",
                    UserName = "AshrafGad542",
                    PhoneNumber = "01009194624"
                };
                await userManager.CreateAsync(user,"Pa$$w0rd");
            }
        }
    }
}
