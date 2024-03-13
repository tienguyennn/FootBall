using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.Model.Migrations
{
    /// <inheritdoc />
    public partial class addPriceAndDes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Field",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c0a20b6-528b-4909-87c5-44fc6824320b", "AQAAAAIAAYagAAAAEPfOWEjiQ7a+yqUnReoR8RDkIQkUzeFdn7ndoJGi3ywpzmMwUnh3bebFoGZnWuSkXA==", "53b023b6-b2e6-40cd-bc1e-de4f309bfb1e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c539559a-4c54-4029-8984-d2755b0e0585", "AQAAAAIAAYagAAAAEDWsaatOlaKJeal4z+0nh+HQX9NA+K5sbJg8kTOej7fuiBqQQgwDXc1sHtB30avAxg==", "97360947-c9e7-4b4c-ab4e-f0a9b4989839" });
        }
    }
}
