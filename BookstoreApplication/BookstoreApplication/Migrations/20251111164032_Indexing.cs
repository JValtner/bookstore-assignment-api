using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class Indexing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuthorAwardBridge_AuthorId",
                table: "AuthorAwardBridge");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83e989cc-c70b-418a-b79d-3302bb53a51d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9df463b-aba1-4b74-8423-4754fc0ae9de");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                newName: "IX_Book_AuthorId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "508c6688-3c8a-42cf-938d-b8709c7ebf76", null, "Librarian", "LIBRARIAN" },
                    { "54b179a1-f50f-4610-b519-9364ef8d0328", null, "Editor", "EDITOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_ISBN",
                table: "Books",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_Title",
                table: "Books",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Author_FullName",
                table: "Authors",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorAward_Author_Award",
                table: "AuthorAwardBridge",
                columns: new[] { "AuthorId", "AwardId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Book_ISBN",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Book_Title",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Author_FullName",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_AuthorAward_Author_Award",
                table: "AuthorAwardBridge");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "508c6688-3c8a-42cf-938d-b8709c7ebf76");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54b179a1-f50f-4610-b519-9364ef8d0328");

            migrationBuilder.RenameIndex(
                name: "IX_Book_AuthorId",
                table: "Books",
                newName: "IX_Books_AuthorId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "83e989cc-c70b-418a-b79d-3302bb53a51d", null, "Editor", "EDITOR" },
                    { "a9df463b-aba1-4b74-8423-4754fc0ae9de", null, "Librarian", "LIBRARIAN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorAwardBridge_AuthorId",
                table: "AuthorAwardBridge",
                column: "AuthorId");
        }
    }
}
