using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace SongsAPI.Data.Migrations
{
    public partial class AddAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Admin role
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { 
                    "1", 
                    "Admin", 
                    "ADMIN",
                    Guid.NewGuid().ToString() 
                }
            );

            // Find piotr's user ID
            var userIdSql = @"
                SELECT Id FROM AspNetUsers 
                WHERE UserName = 'piotr'
                LIMIT 1";

            var userId = migrationBuilder.Sql(userIdSql);

            // Assign Admin role to piotr
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { userId, "1" }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove piotr from Admin role
            migrationBuilder.Sql(@"
                DELETE FROM AspNetUserRoles 
                WHERE RoleId = '1' AND UserId IN (
                    SELECT Id FROM AspNetUsers WHERE UserName = 'piotr'
                )");

            // Delete Admin role
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1"
            );
        }
    }
}
