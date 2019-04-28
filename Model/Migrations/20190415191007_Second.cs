using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Tests_TestID",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TestID",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TestID",
                table: "Questions");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "TestID",
                table: "Questions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestID",
                table: "Questions",
                column: "TestID");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Tests_TestID",
                table: "Questions",
                column: "TestID",
                principalTable: "Tests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
