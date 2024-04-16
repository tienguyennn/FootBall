using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.Model.Migrations
{
    /// <inheritdoc />
    public partial class addFieldId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FieldId",
                table: "Team",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "279104c1-eaa1-49e4-a6e3-f6c902d25ca6", "AQAAAAIAAYagAAAAECFzCwovPEzlCdOAKMsP4lr9JPkwoK7ZWpKL8VnaZ/853KJaVi5ABzy23+Rc8/Gqzw==", "fa8ec740-fc50-4ec9-8138-0b3557e8c05e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldId",
                table: "Team");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab07f25e-9fa3-486e-aef2-8619ec08ed32", "AQAAAAIAAYagAAAAEAihogIjDKhyBX6y1EfYsvGNAxTOpGgyOC9xV1qfB+WEN414lsBvDqK3DNRnmohPhQ==", "72af83a4-7453-47ea-970c-fdc2b0c7ac72" });
        }
    }
}
