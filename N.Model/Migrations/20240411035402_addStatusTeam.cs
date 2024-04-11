using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.Model.Migrations
{
    /// <inheritdoc />
    public partial class addStatusTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Team",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "DepositValue",
                table: "Bookings",
                type: "real",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43375b81-c701-4830-9e20-87e18dbca5cd", "AQAAAAIAAYagAAAAEA9qp9W+FM15/v9xvYS2M1iLc3oJ8TKZ2xTgLqncjO0jZXGg+AN+iThwSJwlxdCuGw==", "548e03ff-3308-4fde-831c-03bb2ccb4696" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "DepositValue",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "144c1320-2de3-482f-85b7-dac55c7889aa", "AQAAAAIAAYagAAAAEMUG5CkUeoDESWGN4RdWc8o+rbd+KEcJsUvILxEmpF8gYwRsIqX61YpJ1PmuuwDU+g==", "d754e1ea-2ef6-4f1e-a300-23a106f4c9bc" });
        }
    }
}
