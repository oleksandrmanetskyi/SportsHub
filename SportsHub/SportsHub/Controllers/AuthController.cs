using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;

namespace SportsHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtTokenService _jwtService;


        public AuthController(IAuthService authService,
            IJwtTokenService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;

        }

        /// <summary>
        /// Authentication of existing user
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _authService.FindByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    return BadRequest("Даного аккаунту не існує");
                }
                var result = await _authService.SignInAsync(loginDto);
                if (result.IsLockedOut)
                {
                    return BadRequest("Аккаунт заблоковано");
                }
                if (result.Succeeded)
                {
                    var generatedToken = await _jwtService.GenerateJwtTokenAsync(user);
                    return Ok(new { token = generatedToken });
                }
                else
                {
                    return BadRequest("Неправильний пароль");
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Registration of new user
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Невалідні дані");
            }
            var registeredUser = await _authService.FindByEmailAsync(registerDto.Email);
            if (registeredUser != null)
            {
                return BadRequest("Такий юзер вже існує");
            }

            var result = await _authService.CreateUserAsync(registerDto);
            if (!result.Succeeded)
            {
                return BadRequest("Некоректний пароль");
            }

            await _authService.AddRoleAsync(registerDto);
            var userDto = await _authService.FindByEmailAsync(registerDto.Email);
            var generatedToken = await _jwtService.GenerateJwtTokenAsync(userDto);
            return Ok(new { token = generatedToken });

        }
    }
}
