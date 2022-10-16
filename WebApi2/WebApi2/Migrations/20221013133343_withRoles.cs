using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi2.Migrations
{
    public partial class withRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LoginUsers",
                columns: new[] { "Id", "Password", "Role", "Username" },
                values: new object[] { 1, "123456", "admin", "admin@mail.ru" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LoginUsers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
