using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceApp.Server.Migrations
{
    public partial class AddAccountVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Category_CategoryId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_CategoryId",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Account",
                newName: "Name");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Category",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "AccountVendorId",
                table: "Account",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Account",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Account",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentAccountId",
                table: "Account",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Projection",
                table: "Account",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "SubAccount",
                table: "Account",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Account",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "AccountVendor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountVendor", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountVendor");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "AccountVendorId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ParentAccountId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Projection",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "SubAccount",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Account",
                newName: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Account_CategoryId",
                table: "Account",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Category_CategoryId",
                table: "Account",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
