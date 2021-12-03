using Microsoft.EntityFrameworkCore.Migrations;

namespace Y.IssueTracker.Web.Migrations
{
    public partial class RenameDefaultAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("635ecf0d-a569-4e94-9c14-29f5d3fcf220"),
                column: "Name",
                value: "Admin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("635ecf0d-a569-4e94-9c14-29f5d3fcf220"),
                column: "Name",
                value: "Administrator");
        }
    }
}
