using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPIs.Migrations
{
    public partial class Applicccc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrow_BookCopy_BookId",
                table: "Borrow");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrow_Employee_ResponsibleById",
                table: "Borrow");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrow_Member_MemberId",
                table: "Borrow");

            migrationBuilder.DropTable(
                name: "Return");

            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_ApplicationUser.MemberIdNumber_MemberId",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Borrow_MemberId",
                table: "Borrow");

            migrationBuilder.DropIndex(
                name: "IX_Borrow_ResponsibleById",
                table: "Borrow");

            migrationBuilder.DropColumn(
                name: "IdNumber",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Borrow");

            migrationBuilder.DropColumn(
                name: "ResponsibleById",
                table: "Borrow");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Borrow",
                newName: "BookCopyId");

            migrationBuilder.RenameIndex(
                name: "IX_Borrow_BookId",
                table: "Borrow",
                newName: "IX_Borrow_BookCopyId");

            migrationBuilder.RenameColumn(
                name: "isBorrowed",
                table: "BookCopy",
                newName: "IsAvailable");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Condition",
                table: "Borrow",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "Borrow",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ResponsibleBy",
                table: "Borrow",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnTime",
                table: "Borrow",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrow_BookCopy_BookCopyId",
                table: "Borrow",
                column: "BookCopyId",
                principalTable: "BookCopy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrow_BookCopy_BookCopyId",
                table: "Borrow");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Borrow");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "Borrow");

            migrationBuilder.DropColumn(
                name: "ResponsibleBy",
                table: "Borrow");

            migrationBuilder.DropColumn(
                name: "ReturnTime",
                table: "Borrow");

            migrationBuilder.RenameColumn(
                name: "BookCopyId",
                table: "Borrow",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Borrow_BookCopyId",
                table: "Borrow",
                newName: "IX_Borrow_BookId");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "BookCopy",
                newName: "isBorrowed");

            migrationBuilder.AddColumn<long>(
                name: "IdNumber",
                table: "Member",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "MemberId",
                table: "Borrow",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsibleById",
                table: "Borrow",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_ApplicationUser.MemberIdNumber_MemberId",
                table: "Member",
                columns: new[] { "IdNumber", "Id" });

            migrationBuilder.CreateTable(
                name: "Return",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowsId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResponsibleById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Condition = table.Column<bool>(type: "bit", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceivDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Return", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Return_Borrow_BorrowsId",
                        column: x => x.BorrowsId,
                        principalTable: "Borrow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Return_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Return_Employee_ResponsibleById",
                        column: x => x.ResponsibleById,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Return_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Borrow_MemberId",
                table: "Borrow",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrow_ResponsibleById",
                table: "Borrow",
                column: "ResponsibleById");

            migrationBuilder.CreateIndex(
                name: "IX_Return_BorrowsId",
                table: "Return",
                column: "BorrowsId");

            migrationBuilder.CreateIndex(
                name: "IX_Return_EmployeeId",
                table: "Return",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Return_MemberId",
                table: "Return",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Return_ResponsibleById",
                table: "Return",
                column: "ResponsibleById");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrow_BookCopy_BookId",
                table: "Borrow",
                column: "BookId",
                principalTable: "BookCopy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrow_Employee_ResponsibleById",
                table: "Borrow",
                column: "ResponsibleById",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrow_Member_MemberId",
                table: "Borrow",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id");
        }
    }
}
