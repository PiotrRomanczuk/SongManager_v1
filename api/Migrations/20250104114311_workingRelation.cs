using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SongsAPI.Migrations
{
    /// <inheritdoc />
    public partial class workingRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Teachers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Teachers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Teachers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Teachers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Teachers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Teachers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Teachers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Teachers",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Teachers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Teachers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Teachers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Teachers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Teachers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Teachers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Songs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_TeacherId",
                table: "Songs",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TeacherId",
                table: "AspNetUsers",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teachers_TeacherId",
                table: "AspNetUsers",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Teachers_TeacherId",
                table: "Songs",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teachers_TeacherId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Teachers_TeacherId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_TeacherId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TeacherId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256);
        }
    }
}
