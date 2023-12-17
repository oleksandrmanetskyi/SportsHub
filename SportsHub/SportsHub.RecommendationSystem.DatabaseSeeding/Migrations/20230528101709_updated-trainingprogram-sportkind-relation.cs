using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsHub.RecommendationSystem.DatabaseSeeding.Migrations
{
    /// <inheritdoc />
    public partial class updatedtrainingprogramsportkindrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_SportKinds_SportKindId",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPrograms_SportKindId",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "SportKindId",
                table: "TrainingPrograms");

            migrationBuilder.CreateTable(
                name: "TrainingProgramSportKinds",
                columns: table => new
                {
                    SportKindId = table.Column<int>(type: "int", nullable: false),
                    TrainingProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingProgramSportKinds", x => new { x.SportKindId, x.TrainingProgramId });
                    table.ForeignKey(
                        name: "FK_TrainingProgramSportKinds_SportKinds_SportKindId",
                        column: x => x.SportKindId,
                        principalTable: "SportKinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingProgramSportKinds_TrainingPrograms_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramSportKinds_TrainingProgramId",
                table: "TrainingProgramSportKinds",
                column: "TrainingProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingProgramSportKinds");

            migrationBuilder.AddColumn<int>(
                name: "SportKindId",
                table: "TrainingPrograms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_SportKindId",
                table: "TrainingPrograms",
                column: "SportKindId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPrograms_SportKinds_SportKindId",
                table: "TrainingPrograms",
                column: "SportKindId",
                principalTable: "SportKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
