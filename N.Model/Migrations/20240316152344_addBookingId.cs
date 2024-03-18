using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.Model.Migrations
{
    /// <inheritdoc />
    public partial class addBookingId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BookingId",
                table: "FeePayment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99013c35-f760-4e95-b7c7-6a3c83c777e5", "AQAAAAIAAYagAAAAEN52v5PGRgZ1m7pdOb2HBZkcinfTC+OntHZF8xzgsO7bdvSD+TqxaXBDG3yeLQGjDQ==", "de0a2e99-f829-4138-b5c1-345a1a37be13" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "FeePayment");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3f49ad2-d320-497e-9a13-9686e730b1d9", "AQAAAAIAAYagAAAAENR9uFHYjvzX5aP8wjr+ynpL4wARqLIg/Br4asM0JIOC3a4ps21ZS0dzfHFhQoxA5w==", "a5edd043-a809-42ed-bfa1-940462e7e908" });
        }
    }
}
