using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsHub.DataAccess.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_SportKinds_SportKindId1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_News_SportKinds_SportKindId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_Shops_SportKinds_SportKindId",
                table: "Shops");

            migrationBuilder.DropForeignKey(
                name: "FK_SportPlaces_SportKinds_SportKindId",
                table: "SportPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_SportKinds_SportKindId1",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPrograms_SportKindId1",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SportKindId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SportKindId1",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "SportKindId1",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_News_SportKinds_SportKindId",
                table: "News",
                column: "SportKindId",
                principalTable: "SportKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_SportKinds_SportKindId",
                table: "Shops",
                column: "SportKindId",
                principalTable: "SportKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SportPlaces_SportKinds_SportKindId",
                table: "SportPlaces",
                column: "SportKindId",
                principalTable: "SportKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_SportKinds_SportKindId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_Shops_SportKinds_SportKindId",
                table: "Shops");

            migrationBuilder.DropForeignKey(
                name: "FK_SportPlaces_SportKinds_SportKindId",
                table: "SportPlaces");

            migrationBuilder.AddColumn<int>(
                name: "SportKindId1",
                table: "TrainingPrograms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SportKindId1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_SportKindId1",
                table: "TrainingPrograms",
                column: "SportKindId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SportKindId1",
                table: "AspNetUsers",
                column: "SportKindId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_SportKinds_SportKindId1",
                table: "AspNetUsers",
                column: "SportKindId1",
                principalTable: "SportKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_SportKinds_SportKindId",
                table: "News",
                column: "SportKindId",
                principalTable: "SportKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_SportKinds_SportKindId",
                table: "Shops",
                column: "SportKindId",
                principalTable: "SportKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportPlaces_SportKinds_SportKindId",
                table: "SportPlaces",
                column: "SportKindId",
                principalTable: "SportKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
