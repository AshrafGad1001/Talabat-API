using Microsoft.AspNetCore.Identity;
using Talabat.core.Entities.Identity;
using Talabat.Repository.Identity;
using Microsoft.Extensions.DependencyInjection;
namespace Talabat.APIs.Extensions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection service)
        {
            service.AddIdentity<AppUser, IdentityRole>(options =>
            {

            }).AddEntityFrameworkStores<AppIdentityDbContext>();
            service.AddAuthentication();

            return service;
        }
    }
}
