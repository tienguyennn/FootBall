using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.Model.Migrations
{
    /// <inheritdoc />
    public partial class changeToFee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e42f3642-5079-4705-94f4-2f304a7d0cef", "AQAAAAIAAYagAAAAENaWxKK5LAHH4FHU9gwGuYVF8hukGwQbZkb1uvm7L4wZIpxJreC+UoTPoQkQiGn/sw==", "ef45a7c6-5fcc-452c-99f3-24c13c55e2a2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99013c35-f760-4e95-b7c7-6a3c83c777e5", "AQAAAAIAAYagAAAAEN52v5PGRgZ1m7pdOb2HBZkcinfTC+OntHZF8xzgsO7bdvSD+TqxaXBDG3yeLQGjDQ==", "de0a2e99-f829-4138-b5c1-345a1a37be13" });
        }
    }
}
