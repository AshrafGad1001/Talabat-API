using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Services;
using Talabat.Controllers;
using Talabat.core.Entities.Identity;

namespace Talabat.APIs.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _siginInManager;
        private readonly ITokenServices _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> siginInManager,
            ITokenServices _tokenService, IMapper mapper)
        {
            this._userManager = userManager;
            this._siginInManager = siginInManager;
            this._tokenService = _tokenService;
            this._mapper = mapper;
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            // Check if email claim is present
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized(new { message = "Email claim is missing" });
            }

            var user = await _userManager.FindByEmailAsync(email);
            // Check if user is found
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                //This Temp Untill Store Token In DB 
                Token = await _tokenService.CreateToken(user, _userManager)
            });

        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO oldAddress)
        {
  
            var UpdatedAddress = _mapper.Map<AddressDTO, Address>(oldAddress);

            var appuser = await _userManager.FindUserWithAddressByEmailAsync(User);

            appuser.Address = UpdatedAddress;
            var Result = await _userManager.UpdateAsync(appuser);
            if (!Result.Succeeded)
                return BadRequest(new ApiResponse(400, "Error Occured During Update Address"));



            return Ok(_mapper.Map<Address, AddressDTO>(appuser.Address)) ;

        }
    }
}
