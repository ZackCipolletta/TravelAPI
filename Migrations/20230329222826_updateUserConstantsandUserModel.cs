using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApi.Migrations
{
    public partial class updateUserConstantsandUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserConstantsId",
                table: "UserModel",
                type: "int",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserModel_UserConstants_UserConstantsId",
                table: "UserModel");

            migrationBuilder.DropIndex(
                name: "IX_UserModel_UserConstantsId",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "UserConstantsId",
                table: "UserModel");
        }
    }
}
