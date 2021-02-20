using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceApp.Server.Migrations
{
    public partial class IndexUserAccountId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_AccountId",
                table: "User",
                column: "AccountId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_AccountId",
                table: "User");
        }
    }
}
