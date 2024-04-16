using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.Model.Migrations
{
    /// <inheritdoc />
    public partial class addUserIdFee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ServiceFee",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab07f25e-9fa3-486e-aef2-8619ec08ed32", "AQAAAAIAAYagAAAAEAihogIjDKhyBX6y1EfYsvGNAxTOpGgyOC9xV1qfB+WEN414lsBvDqK3DNRnmohPhQ==", "72af83a4-7453-47ea-970c-fdc2b0c7ac72" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ServiceFee");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43375b81-c701-4830-9e20-87e18dbca5cd", "AQAAAAIAAYagAAAAEA9qp9W+FM15/v9xvYS2M1iLc3oJ8TKZ2xTgLqncjO0jZXGg+AN+iThwSJwlxdCuGw==", "548e03ff-3308-4fde-831c-03bb2ccb4696" });
        }
    }
}
