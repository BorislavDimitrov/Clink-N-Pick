﻿// <auto-generated />
using System;
using ClickNPick.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClickNPick.Infrastructure.Migrations
{
    [DbContext(typeof(ClickNPickDbContext))]
    [Migration("20240606081504_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClickNPick.Domain.Models.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = "caec59e1-f6c8-44df-a7ba-26f2e1b462c1",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = "9dfb4e76-04f4-42b8-b293-04c544646494",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Books"
                        },
                        new
                        {
                            Id = "2129a9c5-eb21-4736-86ee-c9e22af90615",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Clothing"
                        },
                        new
                        {
                            Id = "db4b2141-fdc3-44e7-a183-6f301e96a779",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Home Appliances"
                        },
                        new
                        {
                            Id = "5281c664-6138-4720-9c45-0640d10fa4eb",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Sports Equipment"
                        },
                        new
                        {
                            Id = "78d56e22-9484-48e1-baa2-0bbe1ae8a9db",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Furniture"
                        },
                        new
                        {
                            Id = "7a8d04f2-44e7-41b8-abc3-0d5b7c36abc3",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Toys"
                        },
                        new
                        {
                            Id = "2a94004b-cd79-461a-8fc4-5a646c424f89",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Beauty Products"
                        },
                        new
                        {
                            Id = "6a38b47c-f5c5-4421-9daa-a8601510b953",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Food & Beverages"
                        },
                        new
                        {
                            Id = "44717001-ad72-490d-9e44-b1d45d563c5d",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Office Supplies"
                        },
                        new
                        {
                            Id = "66c84e08-8867-418e-b325-e482f2d4c715",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "For The Car"
                        });
                });

            modelBuilder.Entity("ClickNPick.Domain.Models.Image", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsThumbnail")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = "99ebfcf5-de58-4826-aaba-cf403c0f5aa9",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            IsThumbnail = false,
                            PublicId = "get634",
                            Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716827378/cool-profile-picture-87h46gcobjl5e4xu_mt6mhi.jpg",
                            UserId = "881a8ad3-3d37-464b-857b-4ae5d4e88898"
                        },
                        new
                        {
                            Id = "63d0564c-ac7a-416f-bef4-60e51ea87423",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            IsThumbnail = false,
                            PublicId = "ge32_gre_4",
                            Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716829465/5e32f2a324306a19834af322_uhj3uq.jpg",
                            UserId = "5557a45d-c13c-4328-94ca-bd482ae4f11c"
                        },
                        new
                        {
                            Id = "d3b340fc-9eda-4d4a-a1b0-7744665cf5e7",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            IsThumbnail = false,
                            PublicId = "ixr4e_abc123",
                            Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620405/Electronic_sonoa1.jpg"
                        },
                        new
                        {
                            Id = "4f5ff3ef-9544-43ee-be7a-9a1546420efa",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            IsThumbnail = false,
                            PublicId = "12e_x7tc123",
                            Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620567/Number-of-Books-Published-Per-Year_icpoaj.jpg"
                        },
                        new
                        {
                            Id = "4fc0b4d2-42a0-4011-b94d-86e4c3af0edd",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            IsThumbnail = false,
                            PublicId = "pht6781_xyz456",
                            Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620636/San-Diego-Plus-Size-Clothing-Stores_adgwqo.jpg"
                        },
                        new
                        {
                            Id = "98571cd2-c3a0-4465-9945-09cdd9ba11ff",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            IsThumbnail = false,
                            PublicId = "pic_789abc",
                            Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620686/istock-1196974664_cbcqpa.jpg"
                        },
                        new
                        {
                            Id = "007ab344-bbf9-4836-97b5-9bc5674fa8eb",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            IsThumbnail = false,
                            PublicId = "zy5h_x1t2n",
                            Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620767/images_ylu4di.jpg"
                        },
                        new
                        {
                            Id = "d03d7013-d67f-4a06-9565-09b4ca17555c",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            IsThumbnail = false,
                            PublicId = "p3x4hge_456def",
                            Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620828/alimentare-arredamento-2_rlgxvn.jpg"
                        },
                        new
                        {
                            Id = "cbbbfb3b-19e8-4cc0-ad42-1607a5c74b62",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            IsThumbnail = false,
                            PublicId = "7845e_dbc123",
                            Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620881/shutterstock_383521510-002-scaled_wqcefn.jpg"
                        },
                        new
                        {
                            Id = "c89552be-4c92-4363-8ebd-c7aaf9f98941",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            IsThumbnail = false,
                            PublicId = "p23c_123jkl",
                            Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620928/Heinens-Health-And-Beauty-products-800x550-1_lmvte5.jpg"
                        },
                        new
                        {
                            Id = "dee622aa-5d79-4f54-aa12-000b59261115",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            IsThumbnail = false,
                            PublicId = "53gfa_abc123",
                            Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716621012/Food_sqrw11.webp"
                        },
                        new
                        {
                            Id = "4efdd35d-1077-49c0-8f14-472bea5b0656",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            IsThumbnail = false,
                            PublicId = "cht3_123jkl",
                            Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716621051/SupremeHomepageImageRight_ihrnvm.jpg"
                        });
                });

            modelBuilder.Entity("ClickNPick.Domain.Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<decimal>("DiscountPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOnDiscount")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPromoted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PromotedUntil")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("Price");

                    b.HasIndex("Title");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ClickNPick.Domain.Models.PromotionPricing", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DurationDays")
                        .HasMaxLength(30)
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PricePerDay")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18,2)")
                        .HasComputedColumnSql("[Price] / [DurationDays]", true);

                    b.HasKey("Id");

                    b.ToTable("PromotionPricings");

                    b.HasData(
                        new
                        {
                            Id = "3142bb82-7f07-4b48-881e-c6de849dc588",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DurationDays = 7,
                            IsDeleted = false,
                            Name = "Basic",
                            Price = 12m,
                            PricePerDay = 0m
                        },
                        new
                        {
                            Id = "17d0b5ae-605a-4077-92d9-4593e4577e0c",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DurationDays = 14,
                            IsDeleted = false,
                            Name = "Standart",
                            Price = 20m,
                            PricePerDay = 0m
                        },
                        new
                        {
                            Id = "dbc24826-47d1-46b9-9dc5-656839fec2bc",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DurationDays = 30,
                            IsDeleted = false,
                            Name = "Premium",
                            Price = 30m,
                            PricePerDay = 0m
                        });
                });

            modelBuilder.Entity("ClickNPick.Domain.Models.ShipmentRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BuyerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DeliveryReceipt")
                        .HasColumnType("bit");

                    b.Property<string>("EmailOnDelivery")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GoodsReceipt")
                        .HasColumnType("bit");

                    b.Property<bool>("InvoiceBeforePayCD")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReceiverName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverOfficeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ShipmentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShipmentStatus")
                        .HasColumnType("int");

                    b.Property<bool>("SmsNotification")
                        .HasColumnType("bit");

                    b.Property<string>("SmsOnDelivery")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SellerId");

                    b.ToTable("ShipmentRequests");
                });

            modelBuilder.Entity("ClickNPick.Domain.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ImageId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "5557a45d-c13c-4328-94ca-bd482ae4f11c",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "cf0cabdc-573d-4ff0-8bd3-744bbd419aa8",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@yopmail.com",
                            EmailConfirmed = true,
                            ImageId = "63d0564c-ac7a-416f-bef4-60e51ea87423",
                            IsDeleted = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@YOPMAIL.COM",
                            NormalizedUserName = "ADMINOVICH",
                            PasswordHash = "AQAAAAIAAYagAAAAEEpecEwgSLuLnOJiQkl/yYDJFSjykvAxsCUZeQOiABzECDU5MTZ+J19W0TdBz8QN7Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "76c94a90-b636-471d-8d5f-5a7cdeff5afb",
                            TwoFactorEnabled = false,
                            UserName = "Adminovich"
                        },
                        new
                        {
                            Id = "881a8ad3-3d37-464b-857b-4ae5d4e88898",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "463268d1-a30e-4580-80ad-c3d113e98d77",
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user@yopmail.com",
                            EmailConfirmed = true,
                            ImageId = "99ebfcf5-de58-4826-aaba-cf403c0f5aa9",
                            IsDeleted = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "USER@YOPMAIL.COM",
                            NormalizedUserName = "USEROVICH",
                            PasswordHash = "AQAAAAIAAYagAAAAEMGH3XnJ+wfNscAkYhg7yqjLErmejsX+shldoDnIoaYVvXb/Ad3XMOWz1vVyuUnsLA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e6b7d1bb-46a1-4e0a-92e7-840d94c61bac",
                            TwoFactorEnabled = false,
                            UserName = "Userovich"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "d1bc12b0-15b8-4492-91bf-e9ea9da685c8",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "5557a45d-c13c-4328-94ca-bd482ae4f11c",
                            RoleId = "d1bc12b0-15b8-4492-91bf-e9ea9da685c8"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ClickNPick.Domain.Models.Image", b =>
                {
                    b.HasOne("ClickNPick.Domain.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ClickNPick.Domain.Models.Product", b =>
                {
                    b.HasOne("ClickNPick.Domain.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ClickNPick.Domain.Models.User", "Creator")
                        .WithMany("Products")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("ClickNPick.Domain.Models.ShipmentRequest", b =>
                {
                    b.HasOne("ClickNPick.Domain.Models.User", "Buyer")
                        .WithMany("ShipmentsAsBuyer")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ClickNPick.Domain.Models.Product", "Product")
                        .WithMany("ShipmentRequests")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ClickNPick.Domain.Models.User", "Seller")
                        .WithMany("ShipmentsAsSeller")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Product");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("ClickNPick.Domain.Models.User", b =>
                {
                    b.HasOne("ClickNPick.Domain.Models.Image", "Image")
                        .WithOne("User")
                        .HasForeignKey("ClickNPick.Domain.Models.User", "ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ClickNPick.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ClickNPick.Domain.Models.User", null)
                        .WithMany("Claims")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ClickNPick.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ClickNPick.Domain.Models.User", null)
                        .WithMany("Logins")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClickNPick.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ClickNPick.Domain.Models.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ClickNPick.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClickNPick.Domain.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ClickNPick.Domain.Models.Image", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("ClickNPick.Domain.Models.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("ShipmentRequests");
                });

            modelBuilder.Entity("ClickNPick.Domain.Models.User", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Logins");

                    b.Navigation("Products");

                    b.Navigation("Roles");

                    b.Navigation("ShipmentsAsBuyer");

                    b.Navigation("ShipmentsAsSeller");
                });
#pragma warning restore 612, 618
        }
    }
}