using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SE1728_HE173252_A3.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Desctiption",
                table: "PostCategories",
                newName: "Description");

            migrationBuilder.InsertData(
                table: "PostCategories",
                columns: new[] { "CategoryID", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Technology", "Technology related posts" },
                    { 2, "Finance", "Finance related posts" },
                    { 3, "Education", "Education related posts" },
                    { 4, "Travel", "Travel related posts" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PostCategories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PostCategories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PostCategories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PostCategories",
                keyColumn: "CategoryID",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "PostCategories",
                newName: "Desctiption");
        }
    }
}
