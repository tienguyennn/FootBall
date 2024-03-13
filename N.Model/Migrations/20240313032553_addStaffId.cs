using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.Model.Migrations
{
    /// <inheritdoc />
    public partial class addStaffId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StaffId",
                table: "Field",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28df8136-90da-482f-a005-37f3687310d6", "AQAAAAIAAYagAAAAEHXOjqKOvd4Ed1wSSnMPFWO8cujwRmjV4wIW9KnbkaxdXZFIoc3CPPiGMgc0QZhAvw==", "99d7ccfe-1ecf-40f0-8f12-4e40fb865338" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Field");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c0a20b6-528b-4909-87c5-44fc6824320b", "AQAAAAIAAYagAAAAEPfOWEjiQ7a+yqUnReoR8RDkIQkUzeFdn7ndoJGi3ywpzmMwUnh3bebFoGZnWuSkXA==", "53b023b6-b2e6-40cd-bc1e-de4f309bfb1e" });
        }
    }
}
