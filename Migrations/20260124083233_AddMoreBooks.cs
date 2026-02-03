
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "ISBN", "IsAvaiable", "IsAvailable", "PublishDate", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 5, "Robert C. Martin", "978-0132350884", true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clean Code" },
                    { 6, "Martin Fowler", "978-0201485677", true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Refactoring" },
                    { 7, "Eric Freeman", "978-0596007126", true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Head First Design Patterns" },
                    { 8, "Jon Skeet", "978-1617294532", true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# in Depth" },
                    { 9, "Adam Freeman", "978-1484254394", true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pro ASP.NET Core MVC" },
                    { 10, "Kyle Simpson", "978-1491904244", true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2017, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "You Don’t Know JS" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 10);
        }
    }
}
