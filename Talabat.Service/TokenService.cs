using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talabat.APIs.Services;
using Talabat.core.Entities.Identity;

namespace Talabat.Service
{
    public class TokenService : ITokenServices
    {
        private readonly IConfiguration Configuration;

        public TokenService(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public async Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager)
        {

            //private Claims (User Defined)
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.DisplayName)
            };

            var userRoles = await userManager.GetRolesAsync(user);
            if (userRoles != null)
            {
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }
            }

            //Secret Key
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]));


            //Registered  Claims 
            var token = new JwtSecurityToken(
                    issuer: Configuration["JWT:ValidIssuer"],
                    audience: Configuration["JWT:VaildAudience"],
                    expires: DateTime.Now.AddDays(double.Parse(Configuration["JWT:DurationInDays"])),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)

                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
