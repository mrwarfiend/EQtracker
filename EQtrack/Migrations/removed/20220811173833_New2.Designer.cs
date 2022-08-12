﻿// <auto-generated />
using System;
using EQtrack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EQtrack.Migrations
{
    [DbContext(typeof(ModelsContext))]
    [Migration("20220811173833_New2")]
    partial class New2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EQtrack.Models.Category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EQtrack.Models.inventory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<bool>("flag")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("toolID")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("toolID");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("EQtrack.Models.RentorInventory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.Property<bool>("check")
                        .HasColumnType("bit");

                    b.Property<int>("count")
                        .HasColumnType("int");

                    b.Property<DateTime>("timeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("toolId")
                        .HasColumnType("int");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("toolId");

                    b.ToTable("RentorInventories");
                });

            modelBuilder.Entity("EQtrack.Models.ReturnTicket", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InventoryId2")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("repairNeeded")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<int>("toolID")
                        .HasColumnType("int");

                    b.Property<string>("userEmail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("toolID");

                    b.ToTable("Returns");
                });

            modelBuilder.Entity("EQtrack.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("InventoryId1")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("toolID")
                        .HasColumnType("int");

                    b.Property<string>("userEmail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("toolID");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("EQtrack.Models.tool", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int?>("categID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("flag")
                        .HasColumnType("bit");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("categID");

                    b.ToTable("Tools");
                });

            modelBuilder.Entity("EQtrack.Models.inventory", b =>
                {
                    b.HasOne("EQtrack.Models.tool", "Tool")
                        .WithMany()
                        .HasForeignKey("toolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tool");
                });

            modelBuilder.Entity("EQtrack.Models.RentorInventory", b =>
                {
                    b.HasOne("EQtrack.Models.tool", "Tools")
                        .WithMany()
                        .HasForeignKey("toolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tools");
                });

            modelBuilder.Entity("EQtrack.Models.ReturnTicket", b =>
                {
                    b.HasOne("EQtrack.Models.tool", "Tool2")
                        .WithMany()
                        .HasForeignKey("toolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tool2");
                });

            modelBuilder.Entity("EQtrack.Models.Ticket", b =>
                {
                    b.HasOne("EQtrack.Models.tool", "Tool")
                        .WithMany()
                        .HasForeignKey("toolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tool");
                });

            modelBuilder.Entity("EQtrack.Models.tool", b =>
                {
                    b.HasOne("EQtrack.Models.Category", "Categ")
                        .WithMany()
                        .HasForeignKey("categID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categ");
                });
#pragma warning restore 612, 618
        }
    }
}