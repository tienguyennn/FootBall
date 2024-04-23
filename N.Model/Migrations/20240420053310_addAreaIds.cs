using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.Model.Migrations
{
    /// <inheritdoc />
    public partial class addAreaIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AreaIds",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AreaIds", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "535f3f40-9e7c-4c9c-bb55-d3375da2475f", "AQAAAAIAAYagAAAAEPXxb4Px1ykSYcxvNX3vRYpP/gYQ6ULhl8KLqLDQ95EIYLhyjd4SWjEcB05SxoS83A==", "85f0278e-37fd-46b2-ba7c-678280a75632" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaIds",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "279104c1-eaa1-49e4-a6e3-f6c902d25ca6", "AQAAAAIAAYagAAAAECFzCwovPEzlCdOAKMsP4lr9JPkwoK7ZWpKL8VnaZ/853KJaVi5ABzy23+Rc8/Gqzw==", "fa8ec740-fc50-4ec9-8138-0b3557e8c05e" });
        }
    }
}
