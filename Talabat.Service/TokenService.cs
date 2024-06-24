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
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager)
        {

            //private Claims (User Defined)
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.DisplayName)
            };

            var userRoles = await userManager.GetRolesAsync(user);
            if (userRoles != null)
            {
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            //Secret Key
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            Console.WriteLine($"CCCCCCCCCCCCCCCCCCCCCCCCCC :{AuthKey}");

            //Registered  Claims 
            var token = new JwtSecurityToken(
                     issuer: _configuration["JWT:ValidIssuer"],
                     audience: _configuration["JWT:ValidAudience"],
                     expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                     claims: authClaims,
                     signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256)
                 );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
