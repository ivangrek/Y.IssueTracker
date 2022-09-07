using Microsoft.EntityFrameworkCore.Migrations;

namespace Y.IssueTracker.Api.Migrations
{
    public partial class UserIsDefautField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("635ecf0d-a569-4e94-9c14-29f5d3fcf220"),
                column: "IsDefault",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Users");
        }
    }
}
