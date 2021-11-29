using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Y.IssueTracker.Web.Migrations
{
    public partial class UpdateUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Name",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("635ecf0d-a569-4e94-9c14-29f5d3fcf220"),
                columns: new[] { "Email", "Password" },
                values: new object[] { "admin@example.com", "dGVzdA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b5d61694-b355-46dc-afa1-2c385d2b3a7d"),
                columns: new[] { "Email", "Password" },
                values: new object[] { "manager@example.com", "dGVzdA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c04ab03d-49f1-4e03-9806-7282f9f61c54"),
                columns: new[] { "Email", "Password" },
                values: new object[] { "user@example.com", "dGVzdA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);
        }
    }
}
