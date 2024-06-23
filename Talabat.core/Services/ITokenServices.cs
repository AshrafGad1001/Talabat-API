using Microsoft.AspNetCore.Identity;
using Talabat.core.Entities.Identity;

namespace Talabat.APIs.Services
{
    public interface ITokenServices
    { 
        Task<string> CreateToken(AppUser user,UserManager<AppUser> userManager);
    }
}
