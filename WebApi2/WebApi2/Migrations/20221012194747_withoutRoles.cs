using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi2.Migrations
{
    public partial class withoutRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginUsers_UserRoles_RoleId",
                table: "LoginUsers");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_LoginUsers_RoleId",
                table: "LoginUsers");

            migrationBuilder.DeleteData(
                table: "LoginUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "LoginUsers");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "LoginUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "LoginUsers");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "LoginUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "user" });

            migrationBuilder.InsertData(
                table: "LoginUsers",
                columns: new[] { "Id", "Password", "RoleId", "Username" },
                values: new object[] { 1, "123456", 1, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_LoginUsers_RoleId",
                table: "LoginUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginUsers_UserRoles_RoleId",
                table: "LoginUsers",
                column: "RoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
