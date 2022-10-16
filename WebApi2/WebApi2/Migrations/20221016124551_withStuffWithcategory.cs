using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi2.Migrations
{
    public partial class withStuffWithcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StuffSet",
                columns: new[] { "Id", "Brand", "Category_Id", "Count", "Model", "OrderId", "Price", "Seria" },
                values: new object[,]
                {
                    { 1, "Nike", 1, 100, "L", null, 1000m, "1.0" },
                    { 2, "Nike", 1, 100, "M", null, 1000m, "1.0" },
                    { 3, "Supremme", 1, 100, "M", null, 10000m, "1.0" },
                    { 4, "Supremme", 1, 100, "XL", null, 10000m, "2.0" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StuffSet",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StuffSet",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StuffSet",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StuffSet",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
