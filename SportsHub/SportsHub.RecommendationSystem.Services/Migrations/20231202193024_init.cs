using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsHub.RecommendationSystem.Services.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrainingProgramId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    IsTrained = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.UserId, x.TrainingProgramId });
                });

            migrationBuilder.CreateTable(
                name: "SvdResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    AverageGlobalRating = table.Column<float>(type: "real", nullable: false),
                    StartedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SvdResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgramBiases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Biase = table.Column<float>(type: "real", nullable: false),
                    SvdResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingProgramBiases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingProgramBiases_SvdResults_SvdResultId",
                        column: x => x.SvdResultId,
                        principalTable: "SvdResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgramFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingProgramIndex = table.Column<int>(type: "int", nullable: false),
                    FeatureIndex = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    SvdResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingProgramFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingProgramFeatures_SvdResults_SvdResultId",
                        column: x => x.SvdResultId,
                        principalTable: "SvdResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBiases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Biase = table.Column<float>(type: "real", nullable: false),
                    SvdResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBiases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBiases_SvdResults_SvdResultId",
                        column: x => x.SvdResultId,
                        principalTable: "SvdResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIndex = table.Column<int>(type: "int", nullable: false),
                    FeatureIndex = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    SvdResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFeatures_SvdResults_SvdResultId",
                        column: x => x.SvdResultId,
                        principalTable: "SvdResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramBiases_SvdResultId",
                table: "TrainingProgramBiases",
                column: "SvdResultId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramFeatures_SvdResultId",
                table: "TrainingProgramFeatures",
                column: "SvdResultId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBiases_SvdResultId",
                table: "UserBiases",
                column: "SvdResultId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeatures_SvdResultId",
                table: "UserFeatures",
                column: "SvdResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "TrainingProgramBiases");

            migrationBuilder.DropTable(
                name: "TrainingProgramFeatures");

            migrationBuilder.DropTable(
                name: "UserBiases");

            migrationBuilder.DropTable(
                name: "UserFeatures");

            migrationBuilder.DropTable(
                name: "SvdResults");
        }
    }
}
