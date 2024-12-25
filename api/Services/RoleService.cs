using Microsoft.AspNetCore.Identity;
using SongsAPI.Models.Users;

namespace SongsAPI.Services
{
    public class RoleService
    {
        private readonly UserManager<Student> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RoleService> _logger;

        public RoleService(
            UserManager<Student> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RoleService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task EnsureAdminRoleExists()
        {
            try
            {
                // Create Admin role if it doesn't exist
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    _logger.LogInformation("Admin role created successfully");
                }

                // Find piotr
                var piotr = await _userManager.FindByNameAsync("piotr");
                if (piotr == null)
                {
                    _logger.LogWarning("User 'piotr' not found");
                    return;
                }

                // Add piotr to Admin role if not already in it
                if (!await _userManager.IsInRoleAsync(piotr, "Admin"))
                {
                    var result = await _userManager.AddToRoleAsync(piotr, "Admin");
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User 'piotr' added to Admin role successfully");
                    }
                    else
                    {
                        _logger.LogError("Failed to add user 'piotr' to Admin role: {Errors}",
                            string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ensuring admin role exists");
                throw;
            }
        }
    }
}
