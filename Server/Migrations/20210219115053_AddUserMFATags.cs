using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceApp.Server.Migrations
{
    public partial class AddUserMFATags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EnableMFA",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasRegisteredMFA",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnableMFA",
                table: "User");

            migrationBuilder.DropColumn(
                name: "HasRegisteredMFA",
                table: "User");
        }
    }
}
