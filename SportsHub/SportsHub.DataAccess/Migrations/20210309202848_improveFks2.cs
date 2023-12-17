using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsHub.DataAccess.Migrations
{
    public partial class improveFks2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TrainingPrograms_TrainingProgramId1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Nutritions_TrainingPrograms_TrainingProgramId2",
                table: "Nutritions");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_AspNetUsers_UserId",
                table: "TrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_Nutritions_NutritionId",
                table: "TrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_SportKinds_SportKindId1",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPrograms_NutritionId",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPrograms_SportKindId1",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPrograms_UserId",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_Nutritions_TrainingProgramId2",
                table: "Nutritions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TrainingProgramId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NutritionId",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "SportKindId1",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "TrainingProgramId2",
                table: "Nutritions");

            migrationBuilder.DropColumn(
                name: "TrainingProgramId1",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Nutritions_TrainingProgramId",
                table: "Nutritions",
                column: "TrainingProgramId",
                unique: true,
                filter: "[TrainingProgramId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TrainingProgramId",
                table: "AspNetUsers",
                column: "TrainingProgramId",
                unique: true,
                filter: "[TrainingProgramId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TrainingPrograms_TrainingProgramId",
                table: "AspNetUsers",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nutritions_TrainingPrograms_TrainingProgramId",
                table: "Nutritions",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TrainingPrograms_TrainingProgramId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Nutritions_TrainingPrograms_TrainingProgramId",
                table: "Nutritions");

            migrationBuilder.DropIndex(
                name: "IX_Nutritions_TrainingProgramId",
                table: "Nutritions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TrainingProgramId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "NutritionId",
                table: "TrainingPrograms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SportKindId1",
                table: "TrainingPrograms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TrainingPrograms",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingProgramId2",
                table: "Nutritions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingProgramId1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_NutritionId",
                table: "TrainingPrograms",
                column: "NutritionId",
                unique: true,
                filter: "[NutritionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_SportKindId1",
                table: "TrainingPrograms",
                column: "SportKindId1");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_UserId",
                table: "TrainingPrograms",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Nutritions_TrainingProgramId2",
                table: "Nutritions",
                column: "TrainingProgramId2");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TrainingProgramId1",
                table: "AspNetUsers",
                column: "TrainingProgramId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TrainingPrograms_TrainingProgramId1",
                table: "AspNetUsers",
                column: "TrainingProgramId1",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nutritions_TrainingPrograms_TrainingProgramId2",
                table: "Nutritions",
                column: "TrainingProgramId2",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPrograms_AspNetUsers_UserId",
                table: "TrainingPrograms",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPrograms_Nutritions_NutritionId",
                table: "TrainingPrograms",
                column: "NutritionId",
                principalTable: "Nutritions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPrograms_SportKinds_SportKindId1",
                table: "TrainingPrograms",
                column: "SportKindId1",
                principalTable: "SportKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
