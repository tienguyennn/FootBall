using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.Model.Migrations
{
    /// <inheritdoc />
    public partial class addPaid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deposited",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d47801d-0f94-41a2-ad44-dc606d71c9ac", "AQAAAAIAAYagAAAAEHa5t/Kw1ujS2WUIjspdAduUqv16cTdeZpoaiY4lfqh/LsjVYfwm1pBWIycbCo6kGQ==", "b72e3e3e-f005-4214-a390-0a9ce117c502" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deposited",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69de1b4c-245d-4904-a247-0f60854817c7", "AQAAAAIAAYagAAAAEGQ7sP73wM5SVsq2ll8d7zQHvl3FVfcum2QmQfmmPREEtkv3zbJSIU6tR5G9l50SqQ==", "aced9cbe-e55a-4b05-b09e-3716c0c837df" });
        }
    }
}
