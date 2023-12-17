using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsHub.DataAccess.Migrations
{
    public partial class improveDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_SportKinds_SportKindId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TrainingPrograms_TrainingProgramId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

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
                name: "FK_TrainingPrograms_Nutritions_NutritionId",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPrograms_NutritionId",
                table: "TrainingPrograms");

            migrationBuilder.AlterColumn<int>(
                name: "NutritionId",
                table: "TrainingPrograms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SportKindId",
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
                name: "TrainingProgramId",
                table: "Nutritions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingProgramId2",
                table: "Nutritions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_NutritionId",
                table: "TrainingPrograms",
                column: "NutritionId",
                unique: true,
                filter: "[NutritionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_SportKindId",
                table: "TrainingPrograms",
                column: "SportKindId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_SportKinds_SportKindId",
                table: "AspNetUsers",
                column: "SportKindId",
                principalTable: "SportKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TrainingPrograms_TrainingProgramId",
                table: "AspNetUsers",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_SportKinds_SportKindId",
                table: "News",
                column: "SportKindId",
                principalTable: "SportKinds",
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
                name: "FK_TrainingPrograms_SportKinds_SportKindId",
                table: "TrainingPrograms",
                column: "SportKindId",
                principalTable: "SportKinds",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_SportKinds_SportKindId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TrainingPrograms_TrainingProgramId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_News_SportKinds_SportKindId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_Nutritions_TrainingPrograms_TrainingProgramId2",
                table: "Nutritions");

            migrationBuilder.DropForeignKey(
                name: "FK_Shops_SportKinds_SportKindId",
                table: "Shops");

            migrationBuilder.DropForeignKey(
                name: "FK_SportPlaces_SportKinds_SportKindId",
                table: "SportPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_AspNetUsers_UserId",
                table: "TrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_Nutritions_NutritionId",
                table: "TrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_SportKinds_SportKindId",
                table: "TrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_SportKinds_SportKindId1",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPrograms_NutritionId",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPrograms_SportKindId",
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

            migrationBuilder.DropColumn(
                name: "SportKindId",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "SportKindId1",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "TrainingProgramId",
                table: "Nutritions");

            migrationBuilder.DropColumn(
                name: "TrainingProgramId2",
                table: "Nutritions");

            migrationBuilder.AlterColumn<int>(
                name: "NutritionId",
                table: "TrainingPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_NutritionId",
                table: "TrainingPrograms",
                column: "NutritionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_SportKinds_SportKindId",
                table: "AspNetUsers",
                column: "SportKindId",
                principalTable: "SportKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TrainingPrograms_TrainingProgramId",
                table: "AspNetUsers",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_TrainingPrograms_Nutritions_NutritionId",
                table: "TrainingPrograms",
                column: "NutritionId",
                principalTable: "Nutritions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
