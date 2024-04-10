using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.Model.Migrations
{
    /// <inheritdoc />
    public partial class addReson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Field",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "144c1320-2de3-482f-85b7-dac55c7889aa", "AQAAAAIAAYagAAAAEMUG5CkUeoDESWGN4RdWc8o+rbd+KEcJsUvILxEmpF8gYwRsIqX61YpJ1PmuuwDU+g==", "d754e1ea-2ef6-4f1e-a300-23a106f4c9bc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Field");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d47801d-0f94-41a2-ad44-dc606d71c9ac", "AQAAAAIAAYagAAAAEHa5t/Kw1ujS2WUIjspdAduUqv16cTdeZpoaiY4lfqh/LsjVYfwm1pBWIycbCo6kGQ==", "b72e3e3e-f005-4214-a390-0a9ce117c502" });
        }
    }
}
