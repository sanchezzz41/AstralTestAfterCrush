using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralTest.Migrations
{
    public partial class AddEnteredUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnteredUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdUser = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnteredUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnteredUsers_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfoAboutEnteredUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EnteredTime = table.Column<DateTime>(nullable: false),
                    IdEnteredUser = table.Column<Guid>(nullable: false),
                    NameOfAction = table.Column<string>(nullable: false),
                    NameOfController = table.Column<string>(nullable: false),
                    ParametrsToAction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoAboutEnteredUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoAboutEnteredUsers_EnteredUsers_IdEnteredUser",
                        column: x => x.IdEnteredUser,
                        principalTable: "EnteredUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnteredUsers_IdUser",
                table: "EnteredUsers",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_InfoAboutEnteredUsers_IdEnteredUser",
                table: "InfoAboutEnteredUsers",
                column: "IdEnteredUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoAboutEnteredUsers");

            migrationBuilder.DropTable(
                name: "EnteredUsers");
        }
    }
}
