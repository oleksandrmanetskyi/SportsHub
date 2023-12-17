using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsHub.DataAccess.Migrations
{
    public partial class recom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendation_AspNetUsers_UserId",
                table: "Recommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_Recommendation_TrainingPrograms_TrainingProgramId",
                table: "Recommendation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recommendation",
                table: "Recommendation");

            migrationBuilder.RenameTable(
                name: "Recommendation",
                newName: "Recommendations");

            migrationBuilder.RenameIndex(
                name: "IX_Recommendation_TrainingProgramId",
                table: "Recommendations",
                newName: "IX_Recommendations_TrainingProgramId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recommendations",
                table: "Recommendations",
                columns: new[] { "UserId", "TrainingProgramId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_AspNetUsers_UserId",
                table: "Recommendations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_TrainingPrograms_TrainingProgramId",
                table: "Recommendations",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_AspNetUsers_UserId",
                table: "Recommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_TrainingPrograms_TrainingProgramId",
                table: "Recommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recommendations",
                table: "Recommendations");

            migrationBuilder.RenameTable(
                name: "Recommendations",
                newName: "Recommendation");

            migrationBuilder.RenameIndex(
                name: "IX_Recommendations_TrainingProgramId",
                table: "Recommendation",
                newName: "IX_Recommendation_TrainingProgramId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recommendation",
                table: "Recommendation",
                columns: new[] { "UserId", "TrainingProgramId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendation_AspNetUsers_UserId",
                table: "Recommendation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendation_TrainingPrograms_TrainingProgramId",
                table: "Recommendation",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
