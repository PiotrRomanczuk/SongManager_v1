using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SongsAPI.Models;
using SongsAPI.Services;

namespace SongsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Student> _userManager;
        private readonly SignInManager<Student> _signInManager;
        private readonly TokenService _tokenService;

        public AuthController(
            UserManager<Student> userManager,
            SignInManager<Student> signInManager,
            TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        private async Task<ActionResult<AuthResponse>> GenerateAuthResponse(Student user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateJwtToken(user);

            return new AuthResponse
            {
                Token = token,
                Email = user.Email ?? string.Empty,
                Name = user.Name,
                Roles = roles.ToList()
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterModel model)
        {
            var user = new Student
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return await GenerateAuthResponse(user);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                return await GenerateAuthResponse(user);
            }

            return Unauthorized("Invalid email or password");
        }
    }
}
