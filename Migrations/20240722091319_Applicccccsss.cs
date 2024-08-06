using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPIs.Migrations
{
    public partial class Applicccccsss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrow_Member_MemberId",
                table: "Borrow");

            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_MemberIdNumber_MemberId",
                table: "Member");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "Borrow",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "MemberIds",
                table: "Borrow",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_ApplicationUser.MemberIdNumber_MemberId",
                table: "AspNetUsers",
                columns: new[] { "IdNumber", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Borrow_MemberIds",
                table: "Borrow",
                column: "MemberIds");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrow_Member_MemberId",
                table: "Borrow",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrow_Member_MemberIds",
                table: "Borrow",
                column: "MemberIds",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrow_Member_MemberId",
                table: "Borrow");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrow_Member_MemberIds",
                table: "Borrow");

            migrationBuilder.DropIndex(
                name: "IX_Borrow_MemberIds",
                table: "Borrow");

            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_ApplicationUser.MemberIdNumber_MemberId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MemberIds",
                table: "Borrow");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "Borrow",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_MemberIdNumber_MemberId",
                table: "Member",
                columns: new[] { "IdNumber", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Borrow_Member_MemberId",
                table: "Borrow",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
