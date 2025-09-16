using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "Birthday", "FullName" },
                values: new object[,]
                {
                    { 1, "Dobitnik Nobelove nagrade za književnost.", new DateTime(1892, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Ivo Andrić" },
                    { 2, "Autor romana Derviš i smrt.", new DateTime(1910, 4, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Mesa Selimović" },
                    { 3, "Poznat po romanu Bašta, pepeo.", new DateTime(1935, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Danilo Kiš" },
                    { 4, "Pisac za decu i odrasle, humorista.", new DateTime(1915, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Branko Ćopić" },
                    { 5, "Prozaista i političar.", new DateTime(1921, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Dobrica Ćosić" }
                });

            migrationBuilder.InsertData(
                table: "Awards",
                columns: new[] { "Id", "Description", "Name", "YearEstablished" },
                values: new object[,]
                {
                    { 1, "Najznačajnija književna nagrada u Srbiji.", "NIN-ova nagrada", 1954 },
                    { 2, "Međunarodna nagrada za književnost.", "Nobelova nagrada za književnost", 1901 },
                    { 3, "Dodeljuje se za priče i romane.", "Andrićeva nagrada", 1975 },
                    { 4, "Nagrada za dečju književnost.", "Zmajeva nagrada", 1958 }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Address", "Name", "Website" },
                values: new object[,]
                {
                    { 1, "Beograd, Srbija", "Laguna", "https://www.laguna.rs" },
                    { 2, "Beograd, Srbija", "Vulkan", "https://www.knjizare-vulkan.rs" },
                    { 3, "Beograd, Srbija", "Dereta", "https://www.dereta.rs" }
                });

            migrationBuilder.InsertData(
                table: "AuthorAwardBridge",
                columns: new[] { "Id", "AuthorId", "AwardId", "AwardedDate" },
                values: new object[,]
                {
                    { 1, 1, 2, new DateTime(1961, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, 3, new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 2, 1, new DateTime(1967, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 2, 3, new DateTime(1971, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 3, 1, new DateTime(1966, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 3, 3, new DateTime(1973, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 3, 4, new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 4, 4, new DateTime(1950, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 4, 3, new DateTime(1960, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 4, 1, new DateTime(1965, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 5, 1, new DateTime(1955, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 5, 2, new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, 5, 3, new DateTime(1978, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 2, 4, new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, 1, 4, new DateTime(1978, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "ISBN", "PageCount", "PublishedDate", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "978000000001", 350, new DateTime(1945, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Na Drini ćuprija" },
                    { 2, 1, "978000000002", 320, new DateTime(1945, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Travnička hronika" },
                    { 3, 2, "978000000003", 400, new DateTime(1966, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Derviš i smrt" },
                    { 4, 2, "978000000004", 380, new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Tvrđava" },
                    { 5, 3, "978000000005", 250, new DateTime(1965, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Bašta, pepeo" },
                    { 6, 3, "978000000006", 200, new DateTime(1969, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Rani jadi" },
                    { 7, 3, "978000000007", 300, new DateTime(1972, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Peščanik" },
                    { 8, 4, "978000000008", 50, new DateTime(1949, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Ježeva kućica" },
                    { 9, 4, "978000000009", 60, new DateTime(1955, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Doživljaji mačka Toše" },
                    { 10, 5, "978000000010", 420, new DateTime(1954, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Koreni" },
                    { 11, 5, "978000000011", 500, new DateTime(1961, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Deobe" },
                    { 12, 5, "978000000012", 600, new DateTime(1972, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Vreme smrti" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
