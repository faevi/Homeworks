using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi2.Migrations
{
    public partial class withStuffWithcategoryV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CategorySet",
                columns: new[] { "Id", "CategoryName" },
                values: new object[] { 1, "Valenki" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategorySet",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
