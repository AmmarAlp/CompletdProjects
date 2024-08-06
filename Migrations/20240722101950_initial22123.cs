using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPIs.Migrations
{
    public partial class initial22123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_ApplicationUser.MemberIdNumber_MemberId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<long>(
                name: "IdNumber",
                table: "Member",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_ApplicationUser.MemberIdNumber_MemberId",
                table: "Member",
                columns: new[] { "IdNumber", "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_ApplicationUser.MemberIdNumber_MemberId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "IdNumber",
                table: "Member");

            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_ApplicationUser.MemberIdNumber_MemberId",
                table: "AspNetUsers",
                columns: new[] { "IdNumber", "Id" });
        }
    }
}
