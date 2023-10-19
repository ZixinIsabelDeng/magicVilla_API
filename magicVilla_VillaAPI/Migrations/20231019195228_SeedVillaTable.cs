using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace magicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Majestic mountaintop estate, lush gardens, opulent rooms, gold-dripped decor, panoramic views, secret passages, grand ballroom, private lake, royal tapestries, enchanted ambiance.", "https://www.arabianbusiness.com/cloud/2021/09/14/GczvHPLj-arabianranches-2-1200x800.jpg", "Royal Villa", 5, 200.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sprawling hillside palace, verdant terraces, luxurious chambers, marble accents, breathtaking vistas, hidden alcoves, expansive courtyard, serene pond, regal drapery, mystical allure.", "https://cdn.henleyglobal.com/storage/app/media/REALESTATES/st-kitts-stunning-villa-with-breathtaking-views/1-8-1.jpeg", "Minimalism Villa", 9, 35000.0, 700, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), " Grand forest manor, vibrant orchards, lavish suites, crystal chandeliers, sweeping landscapes, concealed grottos, magnificent atrium, tranquil waterfall, noble frescoes, magical charm.", "https://static.ojohosts.ca/p/1001/C7020098_0_mbNfqV_p.jpeg", "Forest Villa", 2, 9990.0, 709, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
