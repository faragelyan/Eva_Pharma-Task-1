using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Eva_Pharma_Task1.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    catName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    catOrder = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "catName", "catOrder", "isDeleted" },
                values: new object[,]
                {
                    { 1, "Category 1", 1, false },
                    { 2, "Category 2", 2, false },
                    { 3, "Category 3", 3, false },
                    { 4, "Category 4", 4, false },
                    { 5, "Category 5", 5, false },
                    { 6, "Category 6", 6, false },
                    { 7, "Category 7", 7, false },
                    { 8, "Category 8", 8, false },
                    { 9, "Category 9", 9, false },
                    { 10, "Category 10", 10, false },
                    { 11, "Category 11", 11, false },
                    { 12, "Category 12", 12, false },
                    { 13, "Category 13", 13, false },
                    { 14, "Category 14", 14, false },
                    { 15, "Category 15", 15, false },
                    { 16, "Category 16", 16, false },
                    { 17, "Category 17", 17, false },
                    { 18, "Category 18", 18, false },
                    { 19, "Category 19", 19, false },
                    { 20, "Category 20", 20, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
