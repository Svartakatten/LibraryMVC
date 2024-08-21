using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMVC.Migrations
{
    /// <inheritdoc />
    public partial class FilterView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BookLoans",
                keyColumn: "Id",
                keyValue: 1,
                column: "LoanDate",
                value: new DateTime(2024, 8, 21, 20, 13, 21, 971, DateTimeKind.Local).AddTicks(6223));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BookLoans",
                keyColumn: "Id",
                keyValue: 1,
                column: "LoanDate",
                value: new DateTime(2024, 8, 21, 18, 49, 51, 771, DateTimeKind.Local).AddTicks(6232));
        }
    }
}
