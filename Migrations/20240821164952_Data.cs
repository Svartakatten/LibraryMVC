using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMVC.Migrations
{
    /// <inheritdoc />
    public partial class Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookLoans",
                columns: new[] { "Id", "BookId", "CustomerId", "LoanDate", "ReturnDate" },
                values: new object[] { 1, 1, 1, new DateTime(2024, 8, 21, 18, 49, 51, 771, DateTimeKind.Local).AddTicks(6232), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookLoans",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
