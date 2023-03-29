using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApi.Migrations
{
    public partial class updateUserConstantsandUserModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserModel_UserConstants_UserConstantsId",
                table: "UserModel");

            migrationBuilder.DropTable(
                name: "UserConstants");

            migrationBuilder.DropIndex(
                name: "IX_UserModel_UserConstantsId",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "UserConstantsId",
                table: "UserModel");

            migrationBuilder.CreateTable(
                name: "UserConstant",
                columns: table => new
                {
                    UserConstantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConstant", x => x.UserConstantId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserModel_UserConstantId",
                table: "UserModel",
                column: "UserConstantId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserModel_UserConstant_UserConstantId",
                table: "UserModel",
                column: "UserConstantId",
                principalTable: "UserConstant",
                principalColumn: "UserConstantId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserModel_UserConstant_UserConstantId",
                table: "UserModel");

            migrationBuilder.DropTable(
                name: "UserConstant");

            migrationBuilder.DropIndex(
                name: "IX_UserModel_UserConstantId",
                table: "UserModel");

            migrationBuilder.AddColumn<int>(
                name: "UserConstantsId",
                table: "UserModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserConstants",
                columns: table => new
                {
                    UserConstantsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConstants", x => x.UserConstantsId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserModel_UserConstantsId",
                table: "UserModel",
                column: "UserConstantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserModel_UserConstants_UserConstantsId",
                table: "UserModel",
                column: "UserConstantsId",
                principalTable: "UserConstants",
                principalColumn: "UserConstantsId");
        }
    }
}
