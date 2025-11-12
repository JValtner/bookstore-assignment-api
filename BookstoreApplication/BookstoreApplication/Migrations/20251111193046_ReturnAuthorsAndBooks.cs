using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class ReturnAuthorsAndBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7bf9ecd-7b31-4423-b64f-a24a503ac74b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9306185-11d2-4123-915a-c43e82691a43");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0116bae3-0a68-43d4-95ad-745bafab5401", null, "Librarian", "LIBRARIAN" },
                    { "5919adc2-78ed-4d3f-90f8-a878a730446f", null, "Editor", "EDITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0116bae3-0a68-43d4-95ad-745bafab5401");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5919adc2-78ed-4d3f-90f8-a878a730446f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e7bf9ecd-7b31-4423-b64f-a24a503ac74b", null, "Editor", "EDITOR" },
                    { "e9306185-11d2-4123-915a-c43e82691a43", null, "Librarian", "LIBRARIAN" }
                });
        }
    }
}
