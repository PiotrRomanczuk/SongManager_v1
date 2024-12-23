// using Microsoft.EntityFrameworkCore.Migrations;
// using System;

// namespace SongsAPI.Migrations
// {
//     public partial class UpdateIdType : Migration
//     {
//         protected override void Up(MigrationBuilder migrationBuilder)
//         {
//             // Example of changing the ID type from string to Guid
//             migrationBuilder.AlterColumn<Guid>(
//                 name: "Id",
//                 table: "Songs",
//                 nullable: false,
//                 oldClrType: typeof(string),
//                 oldType: "TEXT");

//             // Add any other schema changes here
//         }

//         protected override void Down(MigrationBuilder migrationBuilder)
//         {
//             // Revert the changes if needed
//             migrationBuilder.AlterColumn<string>(
//                 name: "Id",
//                 table: "Songs",
//                 type: "TEXT",
//                 nullable: false,
//                 oldClrType: typeof(Guid));
//         }
//     }
// }
