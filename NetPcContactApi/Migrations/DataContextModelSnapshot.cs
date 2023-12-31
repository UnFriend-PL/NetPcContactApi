﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetPcContactApi.Database;

#nullable disable

namespace NetPcContactApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NetPcContactApi.Models.Categories.ContactCategory", b =>
                {
                    b.Property<int>("ContactCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactCategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactCategoryId");

                    b.ToTable("ContactCategories");

                    b.HasData(
                        new
                        {
                            ContactCategoryId = 1,
                            Name = "Prywatny"
                        },
                        new
                        {
                            ContactCategoryId = 2,
                            Name = "Służbowy"
                        },
                        new
                        {
                            ContactCategoryId = 3,
                            Name = "Inny"
                        });
                });

            modelBuilder.Entity("NetPcContactApi.Models.Categories.ContactSubCategory", b =>
                {
                    b.Property<int>("ContactSubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactSubCategoryId"));

                    b.Property<int>("ContactCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactSubCategoryId");

                    b.HasIndex("ContactSubCategoryId")
                        .IsUnique();

                    b.ToTable("SubContactCategories");

                    b.HasData(
                        new
                        {
                            ContactSubCategoryId = 1,
                            ContactCategoryId = 2,
                            Name = "Szef"
                        },
                        new
                        {
                            ContactSubCategoryId = 2,
                            ContactCategoryId = 2,
                            Name = "Księgowa"
                        },
                        new
                        {
                            ContactSubCategoryId = 3,
                            ContactCategoryId = 3,
                            Name = "Nauczyciel"
                        });
                });

            modelBuilder.Entity("NetPcContactApi.Models.User.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int>("ContactCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ContactSubCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Birthday = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ContactCategoryId = 1,
                            ContactSubCategoryId = 1,
                            Email = "admin@example",
                            FirstName = "admin",
                            LastName = "admin",
                            PasswordHash = new byte[0],
                            PasswordSalt = new byte[0],
                            Phone = ""
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
