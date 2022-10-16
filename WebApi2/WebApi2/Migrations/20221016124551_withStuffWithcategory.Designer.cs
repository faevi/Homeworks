﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi2.Models;

#nullable disable

namespace WebApi2.Migrations
{
    [DbContext(typeof(UsersContext))]
    [Migration("20221016124551_withStuffWithcategory")]
    partial class withStuffWithcategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApi2.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategorySet");
                });

            modelBuilder.Entity("WebApi2.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Customer_Id")
                        .HasColumnType("int");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderSet");
                });

            modelBuilder.Entity("WebApi2.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Category_Id")
                        .HasColumnType("int");

                    b.Property<int>("Data_Type")
                        .HasColumnType("int");

                    b.Property<string>("Decription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PropertySet");
                });

            modelBuilder.Entity("WebApi2.Models.Stuff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Category_Id")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Seria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("StuffSet");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Nike",
                            Category_Id = 1,
                            Count = 100,
                            Model = "L",
                            Price = 1000m,
                            Seria = "1.0"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Nike",
                            Category_Id = 1,
                            Count = 100,
                            Model = "M",
                            Price = 1000m,
                            Seria = "1.0"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Supremme",
                            Category_Id = 1,
                            Count = 100,
                            Model = "M",
                            Price = 10000m,
                            Seria = "1.0"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Supremme",
                            Category_Id = 1,
                            Count = 100,
                            Model = "XL",
                            Price = 10000m,
                            Seria = "2.0"
                        });
                });

            modelBuilder.Entity("WebApi2.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LoginUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "123456",
                            Role = "admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("WebApi2.Models.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Property_Id")
                        .HasColumnType("int");

                    b.Property<int>("Stuff_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ValeuSet");
                });

            modelBuilder.Entity("WebApi2.Models.Stuff", b =>
                {
                    b.HasOne("WebApi2.Models.Order", null)
                        .WithMany("Stuffs")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("WebApi2.Models.Order", b =>
                {
                    b.Navigation("Stuffs");
                });
#pragma warning restore 612, 618
        }
    }
}
