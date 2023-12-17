using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsHub.DataAccess.Migrations
{
    public partial class UpdateNutrition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nutritions_TrainingPrograms_TrainingProgramId",
                table: "Nutritions");

            migrationBuilder.DropIndex(
                name: "IX_Nutritions_TrainingProgramId",
                table: "Nutritions");

            migrationBuilder.DropColumn(
                name: "TrainingProgramId",
                table: "Nutritions");

            migrationBuilder.AddColumn<int>(
                name: "NutritionId",
                table: "TrainingPrograms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_NutritionId",
                table: "TrainingPrograms",
                column: "NutritionId",
                unique: true,
                filter: "[NutritionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPrograms_Nutritions_NutritionId",
                table: "TrainingPrograms",
                column: "NutritionId",
                principalTable: "Nutritions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_Nutritions_NutritionId",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPrograms_NutritionId",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "NutritionId",
                table: "TrainingPrograms");

            migrationBuilder.AddColumn<int>(
                name: "TrainingProgramId",
                table: "Nutritions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nutritions_TrainingProgramId",
                table: "Nutritions",
                column: "TrainingProgramId",
                unique: true,
                filter: "[TrainingProgramId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Nutritions_TrainingPrograms_TrainingProgramId",
                table: "Nutritions",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
