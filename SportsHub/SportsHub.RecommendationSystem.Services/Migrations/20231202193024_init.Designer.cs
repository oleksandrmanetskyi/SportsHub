﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportsHub.RecommendationSystem.Services.Database;

#nullable disable

namespace SportsHub.RecommendationSystem.Services.Migrations
{
    [DbContext(typeof(RecommendationsDbContext))]
    [Migration("20231202193024_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SportsHub.RecommendationSystem.DTO.SvdResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("AverageGlobalRating")
                        .HasColumnType("real");

                    b.Property<DateTime>("EndedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SvdResults");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DTO.TrainingProgramBiase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Biase")
                        .HasColumnType("real");

                    b.Property<Guid>("SvdResultId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SvdResultId");

                    b.ToTable("TrainingProgramBiases");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DTO.TrainingProgramFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FeatureIndex")
                        .HasColumnType("int");

                    b.Property<Guid>("SvdResultId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TrainingProgramIndex")
                        .HasColumnType("int");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("SvdResultId");

                    b.ToTable("TrainingProgramFeatures");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DTO.UserBiase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Biase")
                        .HasColumnType("real");

                    b.Property<Guid>("SvdResultId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SvdResultId");

                    b.ToTable("UserBiases");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DTO.UserFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FeatureIndex")
                        .HasColumnType("int");

                    b.Property<Guid>("SvdResultId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UserIndex")
                        .HasColumnType("int");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("SvdResultId");

                    b.ToTable("UserFeatures");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.Services.DTO.Rating", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TrainingProgramId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsTrained")
                        .HasColumnType("bit");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "TrainingProgramId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DTO.TrainingProgramBiase", b =>
                {
                    b.HasOne("SportsHub.RecommendationSystem.DTO.SvdResult", "SvdResult")
                        .WithMany()
                        .HasForeignKey("SvdResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SvdResult");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DTO.TrainingProgramFeature", b =>
                {
                    b.HasOne("SportsHub.RecommendationSystem.DTO.SvdResult", "SvdResult")
                        .WithMany()
                        .HasForeignKey("SvdResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SvdResult");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DTO.UserBiase", b =>
                {
                    b.HasOne("SportsHub.RecommendationSystem.DTO.SvdResult", "SvdResult")
                        .WithMany()
                        .HasForeignKey("SvdResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SvdResult");
                });

            modelBuilder.Entity("SportsHub.RecommendationSystem.DTO.UserFeature", b =>
                {
                    b.HasOne("SportsHub.RecommendationSystem.DTO.SvdResult", "SvdResult")
                        .WithMany()
                        .HasForeignKey("SvdResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SvdResult");
                });
#pragma warning restore 612, 618
        }
    }
}
