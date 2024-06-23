using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Services;
using Talabat.Controllers;
using Talabat.core.Entities.Identity;
using Talabat.Service;

namespace Talabat.APIs.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _siginInManager;
        private readonly ITokenServices _tokenService;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> siginInManager,
            ITokenServices _tokenService)
        {
            this._userManager = userManager;
            this._siginInManager = siginInManager;
            this._tokenService = _tokenService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
                return Unauthorized(new ApiResponse(401));

            var Result = await _siginInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!Result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            }); ;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var user = new AppUser()
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email.Split("@")[0]
            };

            var Result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!Result.Succeeded)
                return BadRequest(new ApiResponse(400));

            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            });


        }
    }
}
