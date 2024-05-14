using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.Model.Migrations
{
    /// <inheritdoc />
    public partial class addBookingIdInvite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BookingId",
                table: "Invite",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ef821eb-f790-4e54-b136-57450b899ded", "AQAAAAIAAYagAAAAEGO3rpO7XPE274K9/4uziirAyw7tindJDlONOZd/imMy+mGpnck4p+sheaJrjBOIFA==", "13552a03-a48f-4ce1-b1f2-b19b4bed3a3d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Invite");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "535f3f40-9e7c-4c9c-bb55-d3375da2475f", "AQAAAAIAAYagAAAAEPXxb4Px1ykSYcxvNX3vRYpP/gYQ6ULhl8KLqLDQ95EIYLhyjd4SWjEcB05SxoS83A==", "85f0278e-37fd-46b2-ba7c-678280a75632" });
        }
    }
}
