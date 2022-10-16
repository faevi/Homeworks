using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi2.Migrations
{
    public partial class withStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategorySet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_Id = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertySet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Id = table.Column<int>(type: "int", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Type = table.Column<int>(type: "int", nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertySet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValeuSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Property_Id = table.Column<int>(type: "int", nullable: false),
                    Stuff_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValeuSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StuffSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Id = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StuffSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StuffSet_OrderSet_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderSet",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "LoginUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Username",
                value: "admin");

            migrationBuilder.CreateIndex(
                name: "IX_StuffSet_OrderId",
                table: "StuffSet",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategorySet");

            migrationBuilder.DropTable(
                name: "PropertySet");

            migrationBuilder.DropTable(
                name: "StuffSet");

            migrationBuilder.DropTable(
                name: "ValeuSet");

            migrationBuilder.DropTable(
                name: "OrderSet");

            migrationBuilder.UpdateData(
                table: "LoginUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Username",
                value: "admin@mail.ru");
        }
    }
}
