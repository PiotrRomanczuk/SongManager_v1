using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SongsAPI.Models.Users;
using SongsAPI.Services;
using SongsAPI.Models;

namespace SongsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Student> _userManager;
        private readonly SignInManager<Student> _signInManager;
        private readonly TokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(
            UserManager<Student> userManager,
            SignInManager<Student> signInManager,
            TokenService tokenService,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

        private async Task<ActionResult<AuthResponse>> GenerateAuthResponse(Student user)
        {
            var roles = await _userManager.GetRolesAsync(user) ?? new List<string>();
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
            var newUserId = Guid.NewGuid().ToString();

            var user = new Student
            {
                Id = newUserId,
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Ensure the role exists before adding the user to the role
            if (!await _roleManager.RoleExistsAsync("Student"))
            {
                return BadRequest("Role does not exist.");
            }

            var addRoleResult = await _userManager.AddToRoleAsync(user, "Student");
            if (!addRoleResult.Succeeded)
            {
                return BadRequest(addRoleResult.Errors);
            }

            // Ensure the user is created successfully before proceeding
            var createdUser = await _userManager.FindByEmailAsync(model.Email);
            if (createdUser == null)
            {
                return StatusCode(500, "Failed to retrieve created user");
            }

            return await GenerateAuthResponse(createdUser);
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
