﻿// <auto-generated />
using System;
using BreweryAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BreweryAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241117062900_Classes")]
    partial class Classes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BreweryAPI.Models.Beer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrewerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrewerId");

                    b.ToTable("Beers");
                });

            modelBuilder.Entity("BreweryAPI.Models.Brewery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Breweries");
                });

            modelBuilder.Entity("BreweryAPI.Models.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BeerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<int>("WholesalerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("WholesalerId", "BeerId")
                        .IsUnique();

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("BreweryAPI.Models.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ErrorMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RequestedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("WholesalerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WholesalerId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("BreweryAPI.Models.QuoteDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BeerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LinePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("QuoteId")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("QuoteId");

                    b.ToTable("QuoteDetails");
                });

            modelBuilder.Entity("BreweryAPI.Models.Wholesaler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Wholesalers");
                });

            modelBuilder.Entity("BreweryAPI.Models.Beer", b =>
                {
                    b.HasOne("BreweryAPI.Models.Brewery", "Brewer")
                        .WithMany("Beers")
                        .HasForeignKey("BrewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brewer");
                });

            modelBuilder.Entity("BreweryAPI.Models.Inventory", b =>
                {
                    b.HasOne("BreweryAPI.Models.Beer", "Beer")
                        .WithMany("Inventories")
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BreweryAPI.Models.Wholesaler", "Wholesaler")
                        .WithMany("Inventories")
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("BreweryAPI.Models.Quote", b =>
                {
                    b.HasOne("BreweryAPI.Models.Wholesaler", "Wholesaler")
                        .WithMany()
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("BreweryAPI.Models.QuoteDetail", b =>
                {
                    b.HasOne("BreweryAPI.Models.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BreweryAPI.Models.Quote", "Quote")
                        .WithMany("Details")
                        .HasForeignKey("QuoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Quote");
                });

            modelBuilder.Entity("BreweryAPI.Models.Beer", b =>
                {
                    b.Navigation("Inventories");
                });

            modelBuilder.Entity("BreweryAPI.Models.Brewery", b =>
                {
                    b.Navigation("Beers");
                });

            modelBuilder.Entity("BreweryAPI.Models.Quote", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("BreweryAPI.Models.Wholesaler", b =>
                {
                    b.Navigation("Inventories");
                });
#pragma warning restore 612, 618
        }
    }
}
