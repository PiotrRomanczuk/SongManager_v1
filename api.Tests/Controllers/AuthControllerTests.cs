using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SongsAPI.Controllers;
using SongsAPI.Models;
using SongsAPI.Services;
using Xunit;

namespace SongsAPI.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<UserManager<Student>> _mockUserManager;
        private readonly Mock<SignInManager<Student>> _mockSignInManager;
        private readonly Mock<TokenService> _mockTokenService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            var userStoreMock = new Mock<IUserStore<Student>>();
            _mockUserManager = new Mock<UserManager<Student>>(
                userStoreMock.Object,
                Mock.Of<IOptions<IdentityOptions>>(),
                Mock.Of<IPasswordHasher<Student>>(),
                Array.Empty<IUserValidator<Student>>(),
                Array.Empty<IPasswordValidator<Student>>(),
                Mock.Of<ILookupNormalizer>(),
                Mock.Of<IdentityErrorDescriber>(),
                Mock.Of<IServiceProvider>(),
                Mock.Of<ILogger<UserManager<Student>>>());

            _mockSignInManager = new Mock<SignInManager<Student>>(
                _mockUserManager.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<Student>>(),
                Mock.Of<IOptions<IdentityOptions>>(),
                Mock.Of<ILogger<SignInManager<Student>>>(),
                Mock.Of<IAuthenticationSchemeProvider>(),
                Mock.Of<IUserConfirmation<Student>>());

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x["JwtSettings:SecretKey"]).Returns("your-256-bit-secret-key-here");
            mockConfiguration.Setup(x => x["JwtSettings:ExpirationInMinutes"]).Returns("60");
            mockConfiguration.Setup(x => x["JwtSettings:Issuer"]).Returns("your-issuer");
            mockConfiguration.Setup(x => x["JwtSettings:Audience"]).Returns("your-audience");

            _mockTokenService = new Mock<TokenService>(mockConfiguration.Object);
            _mockTokenService.Setup(x => x.GenerateJwtToken(It.IsAny<Student>()))
                .Returns("test-token");

            _controller = new AuthController(
                _mockUserManager.Object,
                _mockSignInManager.Object,
                _mockTokenService.Object);
        }

        [Fact]
        public async Task Register_ValidModel_ReturnsOkWithToken()
        {
            // Arrange
            var model = new RegisterModel
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };

            var createdUser = new Student
            {
                Id = "testId",
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name
            };

            var roles = new List<string> { "Student" };

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Student>(), model.Password))
                .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(x => x.FindByEmailAsync(model.Email))
                .ReturnsAsync(createdUser);

            _mockUserManager.Setup(x => x.GetRolesAsync(createdUser))
                .ReturnsAsync(roles);

            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<Student>(), "Student"))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register(model);

            // Assert
            var okResult = Assert.IsType<ActionResult<AuthResponse>>(result);
            var authResponse = Assert.IsType<AuthResponse>(okResult.Value);
            Assert.Equal("test-token", authResponse.Token);
            Assert.Equal(model.Email, authResponse.Email);
            Assert.Equal(model.Name, authResponse.Name);
            Assert.Contains("Student", authResponse.Roles);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsOkWithToken()
        {
            // Arrange
            var model = new LoginModel
            {
                Email = "test@example.com",
                Password = "Password123!"
            };

            var user = new Student
            {
                Id = "testId",
                UserName = model.Email,
                Email = model.Email,
                Name = "Test User"
            };

            var roles = new List<string> { "Student" };

            _mockUserManager.Setup(x => x.FindByEmailAsync(model.Email))
                .ReturnsAsync(user);

            _mockSignInManager
                .Setup(x => x.CheckPasswordSignInAsync(user, model.Password, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            _mockUserManager.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(roles);

            // Act
            var result = await _controller.Login(model);

            // Assert
            var okResult = Assert.IsType<ActionResult<AuthResponse>>(result);
            var authResponse = Assert.IsType<AuthResponse>(okResult.Value);
            Assert.Equal("test-token", authResponse.Token);
            Assert.Equal(model.Email, authResponse.Email);
            Assert.Equal("Test User", authResponse.Name);
            Assert.Contains("Student", authResponse.Roles);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var model = new LoginModel
            {
                Email = "test@example.com",
                Password = "WrongPassword"
            };

            var user = new Student
            {
                Id = "testId",
                UserName = model.Email,
                Email = model.Email,
                Name = "Test User"
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(model.Email))
                .ReturnsAsync(user);

            _mockSignInManager
                .Setup(x => x.CheckPasswordSignInAsync(user, model.Password, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            // Act
            var result = await _controller.Login(model);

            // Assert
            var actionResult = Assert.IsType<ActionResult<AuthResponse>>(result);
            Assert.IsType<UnauthorizedObjectResult>(actionResult.Result);
        }
    }
}
