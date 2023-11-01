using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace magicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaID",
                table: "VillaNumber",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 24, 10, 27, 12, 407, DateTimeKind.Local).AddTicks(5631));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 24, 10, 27, 12, 407, DateTimeKind.Local).AddTicks(5692));

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumber_VillaID",
                table: "VillaNumber",
                column: "VillaID");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumber_Villas_VillaID",
                table: "VillaNumber",
                column: "VillaID",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumber_Villas_VillaID",
                table: "VillaNumber");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumber_VillaID",
                table: "VillaNumber");

            migrationBuilder.DropColumn(
                name: "VillaID",
                table: "VillaNumber");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 23, 14, 43, 56, 849, DateTimeKind.Local).AddTicks(3269));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 23, 14, 43, 56, 849, DateTimeKind.Local).AddTicks(3332));
        }
    }
}
