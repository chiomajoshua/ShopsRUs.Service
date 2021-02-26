﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShopsRUs.Service.Core.Storage;

namespace ShopsRUs.Service.Migrations
{
    [DbContext(typeof(ShopsRusDbContext))]
    partial class ShopsRusDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ShopsRUs.Service.Core.Storage.Models.Discounts", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<double>("DiscountPercentage")
                        .HasColumnType("double precision");

                    b.Property<string>("DiscountType")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Discounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("116ddf63-2aae-4f82-8707-9e56e6d56421"),
                            Description = "10% discount for all store affiliates",
                            DiscountPercentage = 10.0,
                            DiscountType = "Affiliate"
                        },
                        new
                        {
                            Id = new Guid("028f4285-ddca-42e7-b101-a9db2f94a3bf"),
                            Description = "5% discount for all customers above 2 years",
                            DiscountPercentage = 5.0,
                            DiscountType = "Customer"
                        },
                        new
                        {
                            Id = new Guid("565d15fc-1ae3-4b81-9aef-e91d0c8f57f3"),
                            Description = "30% discount for all Employees",
                            DiscountPercentage = 30.0,
                            DiscountType = "Employee"
                        });
                });

            modelBuilder.Entity("ShopsRUs.Service.Core.Storage.Models.ShopRusRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("ShopsRUs.Service.Core.Storage.Models.ShopRusUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateEmployed")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateRegistered")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "e552b583-a620-49c6-b59a-8caf06ad8b47",
                            AccessFailedCount = 0,
                            Address = "40 St Gregory College Road, Ibahams",
                            ConcurrencyStamp = "6d1d2d74-e853-438b-9b26-5fcd2cf39f50",
                            DateEmployed = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateRegistered = new DateTime(2018, 2, 26, 14, 16, 13, 202, DateTimeKind.Local).AddTicks(1264),
                            Designation = "Customer",
                            Email = "JaneDoe@gmail.com",
                            EmailConfirmed = false,
                            Enabled = true,
                            FirstName = "Jane",
                            Gender = "Female",
                            IsAdmin = false,
                            LastName = "Doe",
                            LockoutEnabled = false,
                            PhoneNumber = "+23470623459087",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "967b9626-f5bf-4c4d-a38e-6bd6e3e7d6df",
                            Title = "Miss",
                            TwoFactorEnabled = false,
                            UserId = "CUST-92923",
                            UserName = "JaneDoe@gmail.com"
                        },
                        new
                        {
                            Id = "01156f71-9428-4e86-8776-33bd9e7a7c95",
                            AccessFailedCount = 0,
                            Address = "1104 Onion Boulevard,Shabams",
                            ConcurrencyStamp = "c396db23-5a8c-4903-a81d-1bb7e353c3b3",
                            DateEmployed = new DateTime(2021, 2, 26, 14, 16, 13, 204, DateTimeKind.Local).AddTicks(4172),
                            DateRegistered = new DateTime(2021, 2, 26, 14, 16, 13, 204, DateTimeKind.Local).AddTicks(4147),
                            Designation = "Employee",
                            Email = "JohnPeterson@shopsrus.co",
                            EmailConfirmed = false,
                            Enabled = true,
                            FirstName = "John",
                            Gender = "Male",
                            IsAdmin = false,
                            LastName = "Peterson",
                            LockoutEnabled = false,
                            PhoneNumber = "+23470531059087",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f71bea34-3baa-429e-9f5d-fc7232e0b816",
                            Title = "Mr",
                            TwoFactorEnabled = false,
                            UserId = "SHPRUSTF-75413",
                            UserName = "JohnPeterson@shopsrus.co"
                        },
                        new
                        {
                            Id = "31a03320-ee52-411c-a137-3f48351eba4a",
                            AccessFailedCount = 0,
                            Address = "113 Vanilla Crescent, IJGB",
                            ConcurrencyStamp = "732ca533-1bea-447d-94d1-9a67e71713ab",
                            DateEmployed = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateRegistered = new DateTime(2021, 2, 26, 14, 16, 13, 204, DateTimeKind.Local).AddTicks(5050),
                            Designation = "Affiliate",
                            Email = "KayoWalker@hotmail.com",
                            EmailConfirmed = false,
                            Enabled = true,
                            FirstName = "Kayo",
                            Gender = "Male",
                            IsAdmin = false,
                            LastName = "Walker",
                            LockoutEnabled = false,
                            PhoneNumber = "+23470531059087",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "92696fa9-7365-4e50-a518-9a8f9825d314",
                            Title = "Mr",
                            TwoFactorEnabled = false,
                            UserId = "CUST-28818",
                            UserName = "KayoWalker@hotmail.com"
                        });
                });

            modelBuilder.Entity("ShopsRUs.Service.Core.Storage.Models.ShopRusUserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("ShopsRUs.Service.Core.Storage.Models.ShopRusRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ShopsRUs.Service.Core.Storage.Models.ShopRusUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ShopsRUs.Service.Core.Storage.Models.ShopRusUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ShopsRUs.Service.Core.Storage.Models.ShopRusUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShopsRUs.Service.Core.Storage.Models.ShopRusUserRole", b =>
                {
                    b.HasOne("ShopsRUs.Service.Core.Storage.Models.ShopRusRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopsRUs.Service.Core.Storage.Models.ShopRusUser", "Employee")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ShopsRUs.Service.Core.Storage.Models.ShopRusRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("ShopsRUs.Service.Core.Storage.Models.ShopRusUser", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}