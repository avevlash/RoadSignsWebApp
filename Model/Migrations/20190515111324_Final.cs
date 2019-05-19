using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Signs");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Signs",
                nullable: true,
                oldClrType: typeof(byte[]));

            migrationBuilder.AddColumn<int>(
                name: "TypeID",
                table: "Signs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SignTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignTypes", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Signs_TypeID",
                table: "Signs",
                column: "TypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Signs_SignTypes_TypeID",
                table: "Signs",
                column: "TypeID",
                principalTable: "SignTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signs_SignTypes_TypeID",
                table: "Signs");

            migrationBuilder.DropTable(
                name: "SignTypes");

            migrationBuilder.DropIndex(
                name: "IX_Signs_TypeID",
                table: "Signs");

            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "Signs");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Signs",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Signs",
                nullable: false,
                defaultValue: "");
        }
    }
}
