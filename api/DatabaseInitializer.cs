using Microsoft.AspNetCore.Identity;
using SongsAPI.Data;
using SongsAPI.Services;

public static class DatabaseInitializer
{
    public static async Task InitializeDatabaseAsync(IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var songImportService = scope.ServiceProvider.GetRequiredService<SongImportService>();
            context.Database.EnsureCreated();
            await SeedData.Initialize(context, songImportService);
        }
    }

    public static async Task EnsureAdminRoleExistsAsync(IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var roleService = scope.ServiceProvider.GetRequiredService<RoleService>();
            await roleService.EnsureAdminRoleExists();
        }
    }

    public static async Task EnsureRolesCreatedAsync(IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "STUDENT", "ADMIN" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}