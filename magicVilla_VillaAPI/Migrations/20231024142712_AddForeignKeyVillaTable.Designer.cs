﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using magicVilla_VillaAPI.Data;

#nullable disable

namespace magicVilla_VillaAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231024142712_AddForeignKeyVillaTable")]
    partial class AddForeignKeyVillaTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("magicVilla_VillaAPI.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 10, 24, 10, 27, 12, 407, DateTimeKind.Local).AddTicks(5631),
                            Details = "Majestic mountaintop estate, lush gardens, opulent rooms, gold-dripped decor, panoramic views, secret passages, grand ballroom, private lake, royal tapestries, enchanted ambiance.",
                            ImageUrl = "https://www.arabianbusiness.com/cloud/2021/09/14/GczvHPLj-arabianranches-2-1200x800.jpg",
                            Name = "Royal Villa",
                            Occupancy = 5,
                            Rate = 200.0,
                            Sqft = 550,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 10, 24, 10, 27, 12, 407, DateTimeKind.Local).AddTicks(5692),
                            Details = "Sprawling hillside palace, verdant terraces, luxurious chambers, marble accents, breathtaking vistas, hidden alcoves, expansive courtyard, serene pond, regal drapery, mystical allure.",
                            ImageUrl = "https://cdn.henleyglobal.com/storage/app/media/REALESTATES/st-kitts-stunning-villa-with-breathtaking-views/1-8-1.jpeg",
                            Name = "Minimalism Villa",
                            Occupancy = 9,
                            Rate = 35000.0,
                            Sqft = 700,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Amenity = "",
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Details = " Grand forest manor, vibrant orchards, lavish suites, crystal chandeliers, sweeping landscapes, concealed grottos, magnificent atrium, tranquil waterfall, noble frescoes, magical charm.",
                            ImageUrl = "https://static.ojohosts.ca/p/1001/C7020098_0_mbNfqV_p.jpeg",
                            Name = "Forest Villa",
                            Occupancy = 2,
                            Rate = 9990.0,
                            Sqft = 709,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("magicVilla_VillaAPI.Models.VillaNumber", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VillaID")
                        .HasColumnType("int");

                    b.HasKey("VillaNo");

                    b.HasIndex("VillaID");

                    b.ToTable("VillaNumber");
                });

            modelBuilder.Entity("magicVilla_VillaAPI.Models.VillaNumber", b =>
                {
                    b.HasOne("magicVilla_VillaAPI.Models.Villa", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}
