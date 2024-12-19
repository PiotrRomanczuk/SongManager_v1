using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SongsAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSongModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassGroup",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "Songs",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Artist",
                table: "Songs",
                newName: "Ultimate-Guitar Link");

            migrationBuilder.AlterColumn<Guid>(
                name: "FavoriteSongsId",
                table: "StudentSongs",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Songs",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "AudioFiles",
                table: "Songs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Songs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chords",
                table: "Songs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Songs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Songs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortTitle",
                table: "Songs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SongsId",
                table: "LessonSongs",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioFiles",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Chords",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "ShortTitle",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "Ultimate-Guitar Link",
                table: "Songs",
                newName: "Artist");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Songs",
                newName: "DateAdded");

            migrationBuilder.AlterColumn<int>(
                name: "FavoriteSongsId",
                table: "StudentSongs",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Songs",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "ClassGroup",
                table: "Songs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "SongsId",
                table: "LessonSongs",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");
        }
    }
}
