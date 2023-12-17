using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsHub.DataAccess.Migrations
{
    public partial class UpdateRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TrainingProgramId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TrainingProgramId",
                table: "AspNetUsers",
                column: "TrainingProgramId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TrainingProgramId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TrainingProgramId",
                table: "AspNetUsers",
                column: "TrainingProgramId",
                unique: true,
                filter: "[TrainingProgramId] IS NOT NULL");
        }
    }
}
