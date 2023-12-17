using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsHub.DataAccess.Migrations
{
    public partial class improveFks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TrainingPrograms_TrainingProgramId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TrainingProgramId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "TrainingProgramId1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TrainingPrograms_TrainingProgramId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TrainingProgramId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TrainingProgramId1",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TrainingProgramId",
                table: "AspNetUsers",
                column: "TrainingProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TrainingPrograms_TrainingProgramId",
                table: "AspNetUsers",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
