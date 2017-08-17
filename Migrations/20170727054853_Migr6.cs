using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralTest.Migrations
{
    public partial class Migr6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListOfTasks",
                columns: table => new
                {
                    ListId = table.Column<Guid>(nullable: false),
                    NameOfList = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOfTasks", x => x.ListId);
                    table.ForeignKey(
                        name: "FK_ListOfTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyTasks",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(nullable: false),
                    ActualTimeEnd = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    ListId = table.Column<Guid>(nullable: false),
                    TextTask = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyTasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_MyTasks_ListOfTasks_ListId",
                        column: x => x.ListId,
                        principalTable: "ListOfTasks",
                        principalColumn: "ListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListOfTasks_UserId",
                table: "ListOfTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MyTasks_ListId",
                table: "MyTasks",
                column: "ListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyTasks");

            migrationBuilder.DropTable(
                name: "ListOfTasks");
        }
    }
}
