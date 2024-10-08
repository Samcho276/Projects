﻿// <auto-generated />
using System;
using Doniczkomat.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Doniczkomat.Migrations
{
    [DbContext(typeof(SmartPlantPotContext))]
    [Migration("20240619164143_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Doniczkomat.Database.PlantPot", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("cooldown")
                        .HasColumnType("int");

                    b.Property<string>("ip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("lastWatering")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("plantPotID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("soilMoisture")
                        .HasColumnType("int");

                    b.Property<int>("waterLevel")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Pots");
                });
#pragma warning restore 612, 618
        }
    }
}
