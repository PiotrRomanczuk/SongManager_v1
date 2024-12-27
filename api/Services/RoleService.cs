using Microsoft.AspNetCore.Identity;
using SongsAPI.Models.Users;
using Microsoft.Extensions.Logging;

namespace SongsAPI.Services
{
    public class RoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RoleService> _logger;

        public RoleService(
            UserManager<ApplicationUser> userManager,
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

                // Log the user manager instance
                _logger.LogInformation("UserManager instance: {UserManager}", _userManager);

                // Log every user in the _userManager
                foreach (var user in _userManager.Users)
                {
                    _logger.LogInformation("User: {User}", user);
                }

                // Find user 'piotr'
                var piotr = await _userManager.FindByIdAsync("db6a0539-04f6-43ca-8300-f5789cc57506");
                if (piotr == null)
                {
                    _logger.LogWarning("User 'piotr' not found");
                    return;
                }

                // Add 'piotr' to Admin role if not already in it
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
            catch (Microsoft.Data.Sqlite.SqliteException ex) when (ex.SqliteErrorCode == 1 && ex.Message.Contains("no such column: a.Discriminator"))
            {
                _logger.LogError(ex, "SQLite Error: 'no such column: a.Discriminator'. Ensure your migrations are up to date.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ensuring admin role exists");
                throw;
            }
        }
    }
}
