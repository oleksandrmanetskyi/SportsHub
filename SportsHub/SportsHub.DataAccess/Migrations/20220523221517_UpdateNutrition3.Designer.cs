﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportsHub.DataAccess;

namespace SportsHub.DataAccess.Migrations
{
    [DbContext(typeof(SportsHubDbContext))]
    [Migration("20220523221517_UpdateNutrition3")]
    partial class UpdateNutrition3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

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

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
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

            modelBuilder.Entity("SportsHub.DataAccess.Entities.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SportKindId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SportKindId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("SportsHub.DataAccess.Entities.Nutrition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Nutritions");
                });

            modelBuilder.Entity("SportsHub.DataAccess.Entities.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SportKindId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SportKindId");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("SportsHub.DataAccess.Entities.SportKind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SportKinds");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Archery"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Athletics"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Billiards"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Bobsleigh"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Bowling"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Boxing"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Canoe"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Chess"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Climbing"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Cycling"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Dancing"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Darts"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Discus"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Fencing"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Figure"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Skating"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Fishing"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Gliding"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Gymnastics"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Hammer Throwing"
                        },
                        new
                        {
                            Id = 21,
                            Name = "Hang-gliding"
                        },
                        new
                        {
                            Id = 22,
                            Name = "Diving"
                        },
                        new
                        {
                            Id = 23,
                            Name = "High jump"
                        },
                        new
                        {
                            Id = 24,
                            Name = "Hiking"
                        },
                        new
                        {
                            Id = 25,
                            Name = "Horse"
                        },
                        new
                        {
                            Id = 26,
                            Name = "Racing"
                        },
                        new
                        {
                            Id = 27,
                            Name = "Hunting"
                        },
                        new
                        {
                            Id = 28,
                            Name = "Ice Hockey"
                        },
                        new
                        {
                            Id = 29,
                            Name = "Pentathlon"
                        },
                        new
                        {
                            Id = 30,
                            Name = "Motorsports"
                        },
                        new
                        {
                            Id = 31,
                            Name = "Mountaineering"
                        },
                        new
                        {
                            Id = 32,
                            Name = "Paintball"
                        },
                        new
                        {
                            Id = 33,
                            Name = "Parachuting"
                        },
                        new
                        {
                            Id = 34,
                            Name = "Race"
                        },
                        new
                        {
                            Id = 35,
                            Name = "Gymnastics"
                        },
                        new
                        {
                            Id = 36,
                            Name = "Riding"
                        },
                        new
                        {
                            Id = 37,
                            Name = "Skipping"
                        },
                        new
                        {
                            Id = 38,
                            Name = "Rowing"
                        },
                        new
                        {
                            Id = 39,
                            Name = "Running"
                        },
                        new
                        {
                            Id = 40,
                            Name = "Sailing"
                        },
                        new
                        {
                            Id = 41,
                            Name = "Shooting"
                        },
                        new
                        {
                            Id = 42,
                            Name = "Shot put"
                        },
                        new
                        {
                            Id = 43,
                            Name = "Skateboarding"
                        },
                        new
                        {
                            Id = 44,
                            Name = "Skating"
                        },
                        new
                        {
                            Id = 45,
                            Name = "Jumping"
                        },
                        new
                        {
                            Id = 46,
                            Name = "Skiing"
                        },
                        new
                        {
                            Id = 47,
                            Name = "Snowboarding"
                        },
                        new
                        {
                            Id = 48,
                            Name = "Football"
                        },
                        new
                        {
                            Id = 49,
                            Name = "Surfing"
                        },
                        new
                        {
                            Id = 50,
                            Name = "Swimming"
                        },
                        new
                        {
                            Id = 51,
                            Name = "Tennis"
                        },
                        new
                        {
                            Id = 52,
                            Name = "Walking"
                        },
                        new
                        {
                            Id = 53,
                            Name = "Water Polo"
                        },
                        new
                        {
                            Id = 54,
                            Name = "Waterski"
                        },
                        new
                        {
                            Id = 55,
                            Name = "Lifting"
                        },
                        new
                        {
                            Id = 56,
                            Name = "Windsurfing"
                        },
                        new
                        {
                            Id = 57,
                            Name = "Wrestling"
                        });
                });

            modelBuilder.Entity("SportsHub.DataAccess.Entities.SportPlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SportKindId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SportKindId");

                    b.ToTable("SportPlaces");
                });

            modelBuilder.Entity("SportsHub.DataAccess.Entities.TrainingProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NutritionId")
                        .HasColumnType("int");

                    b.Property<int?>("SportKindId")
                        .HasColumnType("int")
                        .HasColumnName("SportKindId");

                    b.HasKey("Id");

                    b.HasIndex("NutritionId")
                        .IsUnique()
                        .HasFilter("[NutritionId] IS NOT NULL");

                    b.HasIndex("SportKindId");

                    b.ToTable("TrainingPrograms");
                });

            modelBuilder.Entity("SportsHub.DataAccess.Entities.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SportKindId")
                        .HasColumnType("int")
                        .HasColumnName("SportKindId");

                    b.Property<string>("SurName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TrainingProgramId")
                        .HasColumnType("int");

                    b.HasIndex("SportKindId");

                    b.HasIndex("TrainingProgramId");

                    b.HasDiscriminator().HasValue("User");
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

            modelBuilder.Entity("SportsHub.DataAccess.Entities.News", b =>
                {
                    b.HasOne("SportsHub.DataAccess.Entities.SportKind", "SportKind")
                        .WithMany("News")
                        .HasForeignKey("SportKindId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SportKind");
                });

            modelBuilder.Entity("SportsHub.DataAccess.Entities.Shop", b =>
                {
                    b.HasOne("SportsHub.DataAccess.Entities.SportKind", "SportKind")
                        .WithMany("Shops")
                        .HasForeignKey("SportKindId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SportKind");
                });

            modelBuilder.Entity("SportsHub.DataAccess.Entities.SportPlace", b =>
                {
                    b.HasOne("SportsHub.DataAccess.Entities.SportKind", "SportKind")
                        .WithMany("SportPlaces")
                        .HasForeignKey("SportKindId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SportKind");
                });

            modelBuilder.Entity("SportsHub.DataAccess.Entities.TrainingProgram", b =>
                {
                    b.HasOne("SportsHub.DataAccess.Entities.Nutrition", "Nutrition")
                        .WithOne("TrainingProgram")
                        .HasForeignKey("SportsHub.DataAccess.Entities.TrainingProgram", "NutritionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SportsHub.DataAccess.Entities.SportKind", "SportKind")
                        .WithMany("TrainingPrograms")
                        .HasForeignKey("SportKindId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Nutrition");

                    b.Navigation("SportKind");
                });

            modelBuilder.Entity("SportsHub.DataAccess.Entities.User", b =>
                {
                    b.HasOne("SportsHub.DataAccess.Entities.SportKind", "SportKind")
                        .WithMany("Users")
                        .HasForeignKey("SportKindId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SportsHub.DataAccess.Entities.TrainingProgram", "TrainingProgram")
                        .WithMany("Users")
                        .HasForeignKey("TrainingProgramId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("SportKind");

                    b.Navigation("TrainingProgram");
                });

            modelBuilder.Entity("SportsHub.DataAccess.Entities.Nutrition", b =>
                {
                    b.Navigation("TrainingProgram");
                });

            modelBuilder.Entity("SportsHub.DataAccess.Entities.SportKind", b =>
                {
                    b.Navigation("News");

                    b.Navigation("Shops");

                    b.Navigation("SportPlaces");

                    b.Navigation("TrainingPrograms");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("SportsHub.DataAccess.Entities.TrainingProgram", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
