using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.core.Entities.Identity;
using Talabat.Repository.Identity;


namespace Talabat.APIs.Extensions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddIdentity<AppUser, IdentityRole>(options =>
            {

            }).AddEntityFrameworkStores<AppIdentityDbContext>();
            //.AddDefaultTokenProviders();

            //JwtBearerDefaults.AuthenticationScheme
            service.AddAuthentication()
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters()
                       {
                           ValidateIssuer = true,
                           ValidIssuer = configuration["JWT:ValidIssuer"],
                           ValidateAudience = true,
                           ValidAudience = configuration["JWT:ValidAudience"],
                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                           ValidateLifetime = true,
                       };
                   });

            return service;
        }
    }
}
