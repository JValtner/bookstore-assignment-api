using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class comicFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "355d274c-5ada-402f-aa44-dbbf72896eec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc1bbf4-921e-4f65-a853-3c65e1dded89");

            migrationBuilder.DropColumn(
                name: "ImageIconUrl",
                table: "LocalIssues");

            migrationBuilder.DropColumn(
                name: "ImageMediumUrl",
                table: "LocalIssues");

            migrationBuilder.DropColumn(
                name: "ImageSuperUrl",
                table: "LocalIssues");

            migrationBuilder.DropColumn(
                name: "VolumeApiUrl",
                table: "LocalIssues");

            migrationBuilder.DropColumn(
                name: "VolumeId",
                table: "LocalIssues");

            migrationBuilder.DropColumn(
                name: "VolumeName",
                table: "LocalIssues");

            migrationBuilder.CreateTable(
                name: "ComicVineImages",
                columns: table => new
                {
                    LocalIssueId = table.Column<int>(type: "integer", nullable: false),
                    Image_IconUrl = table.Column<string>(type: "text", nullable: false),
                    Image_MediumUrl = table.Column<string>(type: "text", nullable: false),
                    Image_SuperUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicVineImages", x => x.LocalIssueId);
                    table.ForeignKey(
                        name: "FK_ComicVineImages_LocalIssues_LocalIssueId",
                        column: x => x.LocalIssueId,
                        principalTable: "LocalIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComicVineVolumes",
                columns: table => new
                {
                    LocalIssueId = table.Column<int>(type: "integer", nullable: false),
                    Volume_Id = table.Column<int>(type: "integer", nullable: false),
                    Volume_Name = table.Column<string>(type: "text", nullable: false),
                    Volume_ApiDetailUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicVineVolumes", x => x.LocalIssueId);
                    table.ForeignKey(
                        name: "FK_ComicVineVolumes_LocalIssues_LocalIssueId",
                        column: x => x.LocalIssueId,
                        principalTable: "LocalIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "19d970cd-b477-4367-9460-c6e2ca086e5f", null, "Editor", "EDITOR" },
                    { "cc04dae4-e8f3-4354-b953-b5d4cdbda432", null, "Librarian", "LIBRARIAN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComicVineImages");

            migrationBuilder.DropTable(
                name: "ComicVineVolumes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19d970cd-b477-4367-9460-c6e2ca086e5f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc04dae4-e8f3-4354-b953-b5d4cdbda432");

            migrationBuilder.AddColumn<string>(
                name: "ImageIconUrl",
                table: "LocalIssues",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageMediumUrl",
                table: "LocalIssues",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSuperUrl",
                table: "LocalIssues",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VolumeApiUrl",
                table: "LocalIssues",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolumeId",
                table: "LocalIssues",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VolumeName",
                table: "LocalIssues",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "355d274c-5ada-402f-aa44-dbbf72896eec", null, "Editor", "EDITOR" },
                    { "abc1bbf4-921e-4f65-a853-3c65e1dded89", null, "Librarian", "LIBRARIAN" }
                });
        }
    }
}
