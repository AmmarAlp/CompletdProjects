using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPIs.Migrations
{
    public partial class deleteeememeberrrıddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdNumber",
                table: "Member");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdNumber",
                table: "Member",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
