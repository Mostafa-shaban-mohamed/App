using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class accountANDlanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Accounts_accountID",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Languages_langID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_accountID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_langID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "accountID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "langID",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Language_ID",
                table: "Users",
                newName: "Language");

            migrationBuilder.RenameColumn(
                name: "Account_ID",
                table: "Users",
                newName: "Account");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Language",
                table: "Users",
                newName: "Language_ID");

            migrationBuilder.RenameColumn(
                name: "Account",
                table: "Users",
                newName: "Account_ID");

            migrationBuilder.AddColumn<string>(
                name: "accountID",
                table: "Users",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "langID",
                table: "Users",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_accountID",
                table: "Users",
                column: "accountID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_langID",
                table: "Users",
                column: "langID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Accounts_accountID",
                table: "Users",
                column: "accountID",
                principalTable: "Accounts",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Languages_langID",
                table: "Users",
                column: "langID",
                principalTable: "Languages",
                principalColumn: "ID");
        }
    }
}
