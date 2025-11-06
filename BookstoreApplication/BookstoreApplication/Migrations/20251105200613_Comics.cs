using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class Comics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f454136-1dc3-4513-aa1e-80080cd1fba4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "acfd9657-8e39-4e34-9b02-18486f324407");

            migrationBuilder.CreateTable(
                name: "LocalIssues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VineId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Deck = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Issue_number = table.Column<int>(type: "integer", nullable: false),
                    VolumeId = table.Column<int>(type: "integer", nullable: true),
                    VolumeName = table.Column<string>(type: "text", nullable: true),
                    VolumeApiUrl = table.Column<string>(type: "text", nullable: true),
                    ImageIconUrl = table.Column<string>(type: "text", nullable: true),
                    ImageMediumUrl = table.Column<string>(type: "text", nullable: true),
                    ImageSuperUrl = table.Column<string>(type: "text", nullable: true),
                    Site_detail_url = table.Column<string>(type: "text", nullable: true),
                    Date_added = table.Column<string>(type: "text", nullable: true),
                    Date_last_updated = table.Column<string>(type: "text", nullable: true),
                    NumberOfPages = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    AvailableCopies = table.Column<int>(type: "integer", nullable: false),
                    Date_local_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalIssues", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "355d274c-5ada-402f-aa44-dbbf72896eec", null, "Editor", "EDITOR" },
                    { "abc1bbf4-921e-4f65-a853-3c65e1dded89", null, "Librarian", "LIBRARIAN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalIssues");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "355d274c-5ada-402f-aa44-dbbf72896eec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc1bbf4-921e-4f65-a853-3c65e1dded89");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f454136-1dc3-4513-aa1e-80080cd1fba4", null, "Librarian", "LIBRARIAN" },
                    { "acfd9657-8e39-4e34-9b02-18486f324407", null, "Editor", "EDITOR" }
                });
        }
    }
}
