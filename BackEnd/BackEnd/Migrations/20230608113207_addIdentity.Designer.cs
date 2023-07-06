﻿// <auto-generated />
using System;
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackEnd.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230608113207_addIdentity")]
    partial class addIdentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackEnd.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActivityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ActivityPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Sorting")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Activitys");
                });

            modelBuilder.Entity("BackEnd.Models.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AreaMinimum")
                        .HasColumnType("int");

                    b.Property<string>("AreaName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AreaRanking")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("BackEnd.Models.BlockArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBlock")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("BlockAreas");
                });

            modelBuilder.Entity("BackEnd.Models.BookedGuestArea", b =>
                {
                    b.Property<int>("GuestId")
                        .HasColumnType("int");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBooking")
                        .HasColumnType("datetime2");

                    b.HasKey("GuestId", "AreaId");

                    b.HasIndex("AreaId");

                    b.ToTable("BookedGuestAreas");
                });

            modelBuilder.Entity("BackEnd.Models.BookingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookingTypes");
                });

            modelBuilder.Entity("BackEnd.Models.DepositWay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DepositWays");
                });

            modelBuilder.Entity("BackEnd.Models.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookingTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBooking")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfDeposit")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DebitNote")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Deposit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("DepositWayId")
                        .HasColumnType("int");

                    b.Property<decimal?>("DiscountByAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DiscountByPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("GrandTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Identifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsCanceled")
                        .HasColumnType("bit");

                    b.Property<int?>("KnowUsId")
                        .HasColumnType("int");

                    b.Property<decimal>("LeftToPay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PaymentCash")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PaymentVisa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TaxIncluded")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TotalCountOfguest")
                        .HasColumnType("int");

                    b.Property<int?>("WhereYouId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingTypeId");

                    b.HasIndex("DepositWayId");

                    b.HasIndex("KnowUsId");

                    b.HasIndex("WhereYouId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("BackEnd.Models.GuestActivity", b =>
                {
                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<int>("GuestId")
                        .HasColumnType("int");

                    b.Property<decimal?>("ActivityPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DateOfBooking")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsIncluded")
                        .HasColumnType("bit");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ActivityId", "GuestId");

                    b.HasIndex("GuestId");

                    b.ToTable("GuestActivities");
                });

            modelBuilder.Entity("BackEnd.Models.GuestTicket", b =>
                {
                    b.Property<int>("GuestId")
                        .HasColumnType("int");

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.Property<int>("CountOfAdult")
                        .HasColumnType("int");

                    b.Property<int>("CountOfKids")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBooking")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("PriceForAdult")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PriceForKids")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("GuestId", "TicketId");

                    b.HasIndex("TicketId");

                    b.ToTable("GuestTickets");
                });

            modelBuilder.Entity("BackEnd.Models.HowYouKnowUs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Sorting")
                        .HasColumnType("int");

                    b.Property<string>("WayName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HowYouKnowUss");
                });

            modelBuilder.Entity("BackEnd.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("PriceForAdult")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PriceForKids")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Sorting")
                        .HasColumnType("int");

                    b.Property<string>("TicketName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tickets");
                });

            modelBuilder.Entity("BackEnd.Models.WhereYouFrom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PlaceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sorting")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("WhereYouFroms");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

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
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
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

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
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

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BackEnd.Models.BlockArea", b =>
                {
                    b.HasOne("BackEnd.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackEnd.Models.BookedGuestArea", b =>
                {
                    b.HasOne("BackEnd.Models.Area", "Area")
                        .WithMany("BookedGuestAreas")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Models.Guest", "Guest")
                        .WithMany("BookedGuestAreas")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackEnd.Models.Guest", b =>
                {
                    b.HasOne("BackEnd.Models.BookingType", "BookingType")
                        .WithMany()
                        .HasForeignKey("BookingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Models.DepositWay", "DepositWay")
                        .WithMany()
                        .HasForeignKey("DepositWayId");

                    b.HasOne("BackEnd.Models.HowYouKnowUs", "HowYouKnowUs")
                        .WithMany()
                        .HasForeignKey("KnowUsId");

                    b.HasOne("BackEnd.Models.WhereYouFrom", "WhereYouFrom")
                        .WithMany()
                        .HasForeignKey("WhereYouId");
                });

            modelBuilder.Entity("BackEnd.Models.GuestActivity", b =>
                {
                    b.HasOne("BackEnd.Models.Activity", "Activity")
                        .WithMany("GuestActivities")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Models.Guest", "Guest")
                        .WithMany("GuestActivities")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackEnd.Models.GuestTicket", b =>
                {
                    b.HasOne("BackEnd.Models.Guest", "Guest")
                        .WithMany("GuestTickets")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Models.Ticket", "Ticket")
                        .WithMany("GuestTickets")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
