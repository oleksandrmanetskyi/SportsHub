﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportsHub.RecommendationSystem.DatabaseSeeding;

#nullable disable

namespace SportsHub.RecommendationSystem.DatabaseSeeding.Migrations
{
    [DbContext(typeof(SportsHubDbContext))]
    [Migration("20230527201156_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SportsHub.RecommendationSystem.DatabaseSeeding.Rating", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.Property<int>("TrainingProgramId")
                        .HasColumnType("int")
                        .HasColumnName("TrainingProgramId");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("UserId", "TrainingProgramId");

                    b.HasIndex("TrainingProgramId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DatabaseSeeding.Recommendation", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.Property<int>("TrainingProgramId")
                        .HasColumnType("int")
                        .HasColumnName("TrainingProgramId");

                    b.Property<float>("ScoreAssumption")
                        .HasColumnType("real");

                    b.HasKey("UserId", "TrainingProgramId");

                    b.HasIndex("TrainingProgramId");

                    b.ToTable("Recommendations");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DatabaseSeeding.SportKind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SportKinds");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DatabaseSeeding.TrainingProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SportKindId")
                        .HasColumnType("int")
                        .HasColumnName("SportKindId");

                    b.HasKey("Id");

                    b.HasIndex("SportKindId");

                    b.ToTable("TrainingPrograms");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DatabaseSeeding.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SportKindId")
                        .HasColumnType("int")
                        .HasColumnName("SportKindId");

                    b.Property<string>("SurName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TrainingProgramId")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SportKindId");

                    b.HasIndex("TrainingProgramId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DatabaseSeeding.Rating", b =>
                {
                    b.HasOne("SportsHub.RecommendationSystem.DatabaseSeeding.TrainingProgram", "TrainingProgram")
                        .WithMany()
                        .HasForeignKey("TrainingProgramId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SportsHub.RecommendationSystem.DatabaseSeeding.User", "user")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TrainingProgram");

                    b.Navigation("user");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DatabaseSeeding.Recommendation", b =>
                {
                    b.HasOne("SportsHub.RecommendationSystem.DatabaseSeeding.TrainingProgram", "TrainingProgram")
                        .WithMany("Recommendations")
                        .HasForeignKey("TrainingProgramId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SportsHub.RecommendationSystem.DatabaseSeeding.User", "user")
                        .WithMany("Recommendations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TrainingProgram");

                    b.Navigation("user");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DatabaseSeeding.TrainingProgram", b =>
                {
                    b.HasOne("SportsHub.RecommendationSystem.DatabaseSeeding.SportKind", "SportKind")
                        .WithMany("TrainingPrograms")
                        .HasForeignKey("SportKindId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("SportKind");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DatabaseSeeding.User", b =>
                {
                    b.HasOne("SportsHub.RecommendationSystem.DatabaseSeeding.SportKind", "SportKind")
                        .WithMany("Users")
                        .HasForeignKey("SportKindId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SportsHub.RecommendationSystem.DatabaseSeeding.TrainingProgram", "TrainingProgram")
                        .WithMany("Users")
                        .HasForeignKey("TrainingProgramId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("SportKind");

                    b.Navigation("TrainingProgram");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DatabaseSeeding.SportKind", b =>
                {
                    b.Navigation("TrainingPrograms");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DatabaseSeeding.TrainingProgram", b =>
                {
                    b.Navigation("Recommendations");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DatabaseSeeding.User", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("Recommendations");
                });
#pragma warning restore 612, 618
        }
    }
}