using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.Model.Migrations
{
    /// <inheritdoc />
    public partial class addFeePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Field",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Bookings",
                type: "real",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FeePayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeePayment", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3f49ad2-d320-497e-9a13-9686e730b1d9", "AQAAAAIAAYagAAAAENR9uFHYjvzX5aP8wjr+ynpL4wARqLIg/Br4asM0JIOC3a4ps21ZS0dzfHFhQoxA5w==", "a5edd043-a809-42ed-bfa1-940462e7e908" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeePayment");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0df800e5-4d42-48a2-b64e-083947a3df69", "AQAAAAIAAYagAAAAEP5PzwMlnvXMExKciw5QGoT/blKTXLKJp8hw8cf9DQL+xv5VGefH3IkrQtiw4QkFPQ==", "d21b5e8a-c4b8-4ec4-8ec5-8930e63f2d03" });
        }
    }
}
