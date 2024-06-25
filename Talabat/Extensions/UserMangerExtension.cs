using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.core.Entities.Identity;

namespace Talabat.APIs.Extensions
{
    public static class UserMangerExtension
    {
        public static async Task<AppUser> FindUserWithAddressByEmailAsync
            (this UserManager<AppUser> userManager, ClaimsPrincipal user)

        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            var CurrentUser = await userManager.Users.Include(U => U.Address)
                                                     .SingleOrDefaultAsync(U => U.Email == email);



            return CurrentUser;
        }
    }
}
