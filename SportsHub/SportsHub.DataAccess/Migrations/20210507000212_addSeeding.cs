using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsHub.DataAccess.Migrations
{
    public partial class addSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SportKinds",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Archery" },
                    { 31, "Mountaineering" },
                    { 32, "Paintball" },
                    { 33, "Parachuting" },
                    { 34, "Race" },
                    { 35, "Gymnastics" },
                    { 36, "Riding" },
                    { 37, "Skipping" },
                    { 38, "Rowing" },
                    { 39, "Running" },
                    { 40, "Sailing" },
                    { 41, "Shooting" },
                    { 42, "Shot put" },
                    { 43, "Skateboarding" },
                    { 44, "Skating" },
                    { 45, "Jumping" },
                    { 46, "Skiing" },
                    { 47, "Snowboarding" },
                    { 48, "Football" },
                    { 49, "Surfing" },
                    { 50, "Swimming" },
                    { 51, "Tennis" },
                    { 52, "Walking" },
                    { 53, "Water Polo" },
                    { 54, "Waterski" },
                    { 55, "Lifting" },
                    { 30, "Motorsports" },
                    { 56, "Windsurfing" },
                    { 29, "Pentathlon" },
                    { 27, "Hunting" },
                    { 2, "Athletics" },
                    { 3, "Billiards" },
                    { 4, "Bobsleigh" },
                    { 5, "Bowling" },
                    { 6, "Boxing" },
                    { 7, "Canoe" },
                    { 8, "Chess" },
                    { 9, "Climbing" },
                    { 10, "Cycling" },
                    { 11, "Dancing" },
                    { 12, "Darts" },
                    { 13, "Discus" }
                });

            migrationBuilder.InsertData(
                table: "SportKinds",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 14, "Fencing" },
                    { 15, "Figure" },
                    { 16, "Skating" },
                    { 17, "Fishing" },
                    { 18, "Gliding" },
                    { 19, "Gymnastics" },
                    { 20, "Hammer Throwing" },
                    { 21, "Hang-gliding" },
                    { 22, "Diving" },
                    { 23, "High jump" },
                    { 24, "Hiking" },
                    { 25, "Horse" },
                    { 26, "Racing" },
                    { 28, "Ice Hockey" },
                    { 57, "Wrestling" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "SportKinds",
                keyColumn: "Id",
                keyValue: 57);
        }
    }
}
