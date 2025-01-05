// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using SongsAPI.Models.Users;
// using SongsAPI.Services;

// namespace SongsAPI.Controllers
// {
//     [Route("api/student/[controller]")]
//     [ApiController]
//     public class StudentAuthController : ControllerBase
//     {
//         private readonly UserManager<Student> _userManager;
//         private readonly SignInManager<Student> _signInManager;
//         private readonly TokenService _tokenService;

//         public StudentAuthController(
//             UserManager<Student> userManager,
//             SignInManager<Student> signInManager,
//             TokenService tokenService)
//         {
//             _userManager = userManager;
//             _signInManager = signInManager;
//             _tokenService = tokenService;
//         }

//         private async Task<ActionResult<AuthResponse>> GenerateAuthResponse(Student user)
//         {
//             var roles = await _userManager.GetRolesAsync(user) ?? new List<string>();
//             var token = _tokenService.GenerateJwtToken(user);

//             return new AuthResponse
//             {
//                 Token = token,
//                 Email = user.Email ?? string.Empty,
//                 Name = user.Name,
//                 Roles = roles.ToList()
//             };
//         }

//         [HttpPost("register")]
//         public async Task<ActionResult<AuthResponse>> Register(RegisterModel model)
//         {
//             var user = new Student
//             {
//                 UserName = model.Email,
//                 Email = model.Email,
//                 Name = model.Name
//             };

//             var result = await _userManager.CreateAsync(user, model.Password);
//             if (!result.Succeeded)
//             {
//                 return BadRequest(result.Errors);
//             }

//             var addRoleResult = await _userManager.AddToRoleAsync(user, "Student");
//             if (!addRoleResult.Succeeded)
//             {
//                 return BadRequest(addRoleResult.Errors);
//             }

//             var createdUser = await _userManager.FindByEmailAsync(model.Email);
//             if (createdUser == null)
//             {
//                 return StatusCode(500, "Failed to retrieve created user");
//             }

//             return await GenerateAuthResponse(createdUser);
//         }

//         [HttpPost("login")]
//         public async Task<ActionResult<AuthResponse>> Login(LoginModel model)
//         {
//             var user = await _userManager.FindByEmailAsync(model.Email);
//             if (user == null)
//             {
//                 return Unauthorized("Invalid email or password");
//             }

//             var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
//             if (result.Succeeded)
//             {
//                 return await GenerateAuthResponse(user);
//             }

//             return Unauthorized("Invalid email or password");
//         }
//     }
// }
